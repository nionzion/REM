using Newtonsoft.Json;
using Npgsql;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace REM
{
    public class DbAccess
    {

        private static DbAccess dbAccess { get; set; } = new DbAccess();

        public NpgsqlConnection Connection { get; set; }

        /// <summary>
        /// Singleton
        /// </summary>
        public static DbAccess Shared => dbAccess;

        public DbAccess()
        {
            using (StreamReader file = new StreamReader(Environment.CurrentDirectory + "/database.json"))
            {
                string json = file.ReadToEnd();
                var parameters = JsonConvert.DeserializeObject<DatabaseParameters>(json);

                var connectionString = $"Server={parameters.Server};port={parameters.Port};User Id={parameters.User};Password={parameters.Password};Database={parameters.Database}";
                Connection = new NpgsqlConnection(connectionString);
            }
        }


        #region Agents 

        public List<EstateAgent> FetchAgents()
        {
            var agents = new List<EstateAgent>();
            var query = "SELECT * FROM vsisp38.estate_agent";
            Connection.Open();
            var cmd = new NpgsqlCommand(query, Connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                agents.Add(new EstateAgent { ID = reader.GetInt16(0).ToString(), Login = reader.GetString(1), Name = reader.GetString(2), Address = reader.GetString(3), Password = reader.GetString(4) });
            }
            Connection.Close();

            return agents;

        }

        public bool CreateAgent(EstateAgent agent)
        {
            try
            {
                if (Connection.State != System.Data.ConnectionState.Open)
                    Connection.Open();

                var query = $"insert into vsisp38.estate_agent(login, name, address, password) values(@login, @name, @address, @password)";
                var cmd = new NpgsqlCommand(query, Connection);
                cmd.Parameters.AddWithValue("login", agent.Login);
                cmd.Parameters.AddWithValue("name", agent.Name);
                cmd.Parameters.AddWithValue("address", agent.Address);
                cmd.Parameters.AddWithValue("password", agent.Password);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Connection.Close();
                GuiState.RaiseAgentsChanged();
                return true;
            }
            catch (Exception xcpt)
            {
                System.Windows.MessageBox.Show(xcpt.Message);
                return false;
            }

        }

        public bool DeleteAgent(EstateAgent agent)
        {
            try
            {
                if (Connection.State != System.Data.ConnectionState.Open)
                    Connection.Open();

                var query = $"delete from vsisp38.estate_agent where login=@login";
                var cmd = new NpgsqlCommand(query, Connection);
                cmd.Parameters.AddWithValue("login", agent.Login);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Connection.Close();
                GuiState.RaiseAgentsChanged();
                return true;
            }
            catch (Exception xcpt)
            {
                System.Windows.MessageBox.Show(xcpt.Message);
                return false;
            }
        }

        public bool UpdateAgent(EstateAgent agent, string key)
        {
            try
            {
                if (Connection.State != System.Data.ConnectionState.Open)
                    Connection.Open();

                var query = $"update vsisp38.estate_agent set login = @login, name = @name, address = @address, password = @password where login = @key";
                var cmd = new NpgsqlCommand(query, Connection);
                cmd.Parameters.AddWithValue("login", agent.Login);
                cmd.Parameters.AddWithValue("name", agent.Name);
                cmd.Parameters.AddWithValue("address", agent.Address);
                cmd.Parameters.AddWithValue("password", agent.Password);
                cmd.Parameters.AddWithValue("key", key);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Connection.Close();
                GuiState.RaiseAgentsChanged();
                return true;
            }
            catch (Exception xcpt)
            {
                System.Windows.MessageBox.Show(xcpt.Message);
                return false;
            }
        }

        public bool ValidateAgent(string username, string password)
        {
            return FetchAgents().FirstOrDefault(agent => agent.Login.ToLower() == username.ToLower() && agent.Password == password) != null;
        }

        #endregion

        #region Persons
        public List<Person> FetchPersons()
        {
            var persons = new List<Person>();
            var query = "SELECT * FROM vsisp38.person";
            if (Connection.State != System.Data.ConnectionState.Open)
                Connection.Open();
            var cmd = new NpgsqlCommand(query, Connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                persons.Add(new Person { ID = reader.GetInt16(0).ToString(), FirstName = reader.GetString(2), LastName= reader.GetString(1), Address = reader.GetString(3) });
            }
            Connection.Close();

            return persons;
        }

        public bool CreatePerson(Person person)
        {
            try
            {
                if(Connection.State != System.Data.ConnectionState.Open) 
                    Connection.Open();

                var query = $"insert into vsisp38.person(first_name, last_name, address) values(@first_name, @last_name, @address)";
                var cmd = new NpgsqlCommand(query, Connection);
                cmd.Parameters.AddWithValue("first_name", person.FirstName);
                cmd.Parameters.AddWithValue("last_name", person.LastName);
                cmd.Parameters.AddWithValue("address", person.Address);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                GuiState.RaisePersonsChanged();
                return true;
            }
            catch (Exception xcpt)
            {
                System.Windows.MessageBox.Show(xcpt.Message);
                return false;
            }
            finally
            {
                Connection.Close();
            }
        }

        #endregion

        #region Estates

        public List<IEstate> FetchEstates()
        {
            var apartments = new List<Apartment>();
            var houses = new List<House>();

            var query = "SELECT apartment.id, apartment.city, apartment.postal_code, apartment.street, apartment.street_no, apartment.square_area, apartment.floor, apartment.rent, apartment.rooms, apartment.balcony, apartment.kitchen, estate_agent.id, estate_agent.name FROM vsisp38.apartment INNER JOIN vsisp38.estate_agent ON apartment.agentId = estate_agent.id";
            if (Connection.State != System.Data.ConnectionState.Open)
                Connection.Open();
            var cmd = new NpgsqlCommand(query, Connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                apartments.Add(new Apartment
                {
                    ID = reader.GetInt16(0).ToString(),
                    City = reader.GetString(1),
                    PostalCode = reader.GetString(2),
                    Street = reader.GetString(3),
                    StreetNumber = reader.GetString(4),
                    SquareArea = reader.GetDouble(5),
                    Floor = reader.GetString(6),
                    Rent = reader.GetDouble(7),
                    Rooms = reader.GetDouble(8),
                    Balcony = reader.GetBoolean(9),
                    Kitchen = reader.GetBoolean(10),
                    Agent = new EstateAgent { ID = reader.GetInt16(11).ToString(), Name = reader.GetString(12) }
                });

            }
            Connection.Close();

            var query2 = "SELECT house.id, city, postal_code, street, street_no, square_area, floors, price, garden, estate_agent.id, estate_agent.name FROM vsisp38.house INNER JOIN vsisp38.estate_agent ON house.agentId = estate_agent.id";

            if (Connection.State != System.Data.ConnectionState.Open)
                Connection.Open();
            var cmd2 = new NpgsqlCommand(query2, Connection);
            var reader2 = cmd2.ExecuteReader();
            while (reader2.Read())
            {
                houses.Add(new House
                {
                    ID = reader2.GetInt16(0).ToString(),
                    City = reader2.GetString(1),
                    PostalCode = reader2.GetString(2),
                    Street = reader2.GetString(3),
                    StreetNumber = reader2.GetString(4),
                    SquareArea = reader2.GetDouble(5),
                    Floors = reader2.GetInt16(6),
                    Price = reader2.GetDouble(7),
                    Garden = reader2.GetBoolean(8),
                    Agent = new EstateAgent { ID = reader.GetInt16(9).ToString(), Name = reader.GetString(10) }

                });
            }
            Connection.Close();

            var estates = new List<IEstate>();
            estates.AddRange(houses);
            estates.AddRange(apartments);
            return estates.OrderBy(e => e.ID).ToList();
        }

        public bool CreateApartment(Apartment apartment)
        {
            try
            {
                if (Connection.State != System.Data.ConnectionState.Open)
                    Connection.Open();

                var query = $"insert into vsisp38.apartment (city, postal_code, street, street_no, square_area, floor, rent, rooms, balcony, kitchen, agentid) VALUES(@city, @postal_code, @street, @street_no, @square_area, @floor, @rent, @rooms, @balcony, @kitchen, @agentId)";
                var cmd = new NpgsqlCommand(query, Connection);
                cmd.Parameters.AddWithValue("city", apartment.City);
                cmd.Parameters.AddWithValue("postal_code", apartment.PostalCode);
                cmd.Parameters.AddWithValue("street", apartment.Street);
                cmd.Parameters.AddWithValue("street_no", apartment.StreetNumber);
                cmd.Parameters.AddWithValue("square_area", apartment.SquareArea);
                cmd.Parameters.AddWithValue("floor", apartment.Floor);
                cmd.Parameters.AddWithValue("rent", apartment.Rent);
                cmd.Parameters.AddWithValue("rooms", apartment.Rooms);
                cmd.Parameters.AddWithValue("balcony", apartment.Balcony);
                cmd.Parameters.AddWithValue("kitchen", apartment.Kitchen);
                cmd.Parameters.AddWithValue("agentId", int.Parse(apartment.Agent.ID));
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Connection.Close();
                GuiState.RaiseEstatesChanged();
                return true;
            }
            catch (Exception xcpt)
            {
                System.Windows.MessageBox.Show(xcpt.Message);
                return false;
            }
        }

        public bool DeleteApartment(Apartment apartment)
        {
            try
            {
                if (Connection.State != System.Data.ConnectionState.Open)
                    Connection.Open();

                var query = $"delete from vsisp38.apartment where id=@id";
                var cmd = new NpgsqlCommand(query, Connection);
                cmd.Parameters.AddWithValue("id", int.Parse(apartment.ID));
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Connection.Close();
                GuiState.RaiseEstatesChanged();
                return true;
            }
            catch (Exception xcpt)
            {
                System.Windows.MessageBox.Show(xcpt.Message);
                return false;
            }
        }

        public bool UpdateApartment(Apartment apartment, string key)
        {
            try
            {
                if (Connection.State != System.Data.ConnectionState.Open)
                    Connection.Open();

                var query = $"update vsisp38.apartment set city = @city, postal_code = @postal_code, street = @street, street_no = @street_no, square_area = @square_area, floor = @floor, rent = @rent, rooms = @rooms, balcony = @balcony, kitchen = @kitchen, agentid = @agentId where id = @id";
                var cmd = new NpgsqlCommand(query, Connection);
                cmd.Parameters.AddWithValue("city", apartment.City);
                cmd.Parameters.AddWithValue("postal_code", apartment.PostalCode);
                cmd.Parameters.AddWithValue("street", apartment.Street);
                cmd.Parameters.AddWithValue("street_no", apartment.StreetNumber);
                cmd.Parameters.AddWithValue("square_area", apartment.SquareArea);
                cmd.Parameters.AddWithValue("floor", apartment.Floor);
                cmd.Parameters.AddWithValue("rent", apartment.Rent);
                cmd.Parameters.AddWithValue("rooms", apartment.Rooms);
                cmd.Parameters.AddWithValue("balcony", apartment.Balcony);
                cmd.Parameters.AddWithValue("kitchen", apartment.Kitchen);
                cmd.Parameters.AddWithValue("agentId", int.Parse(apartment.Agent.ID));
                cmd.Parameters.AddWithValue("id", int.Parse(key));
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Connection.Close();
                GuiState.RaiseEstatesChanged();
                return true;
            }
            catch (Exception xcpt)
            {
                System.Windows.MessageBox.Show(xcpt.Message);
                return false;
            }
        }

        public bool CreateHouse(House house)
        {
            try
            {
                if (Connection.State != System.Data.ConnectionState.Open)
                    Connection.Open();

                var query = $"insert into vsisp38.house (city, postal_code, street, street_no, square_area, floors, price, garden, agentid) VALUES(@city, @postal_code, @street, @street_no, @square_area, @floors, @price, @garden, @agentId)";
                var cmd = new NpgsqlCommand(query, Connection);
                cmd.Parameters.AddWithValue("city", house.City);
                cmd.Parameters.AddWithValue("postal_code", house.PostalCode);
                cmd.Parameters.AddWithValue("street", house.Street);
                cmd.Parameters.AddWithValue("street_no", house.StreetNumber);
                cmd.Parameters.AddWithValue("square_area", house.SquareArea);
                cmd.Parameters.AddWithValue("floors", house.Floors);
                cmd.Parameters.AddWithValue("price", house.Price);
                cmd.Parameters.AddWithValue("garden", house.Garden);
                cmd.Parameters.AddWithValue("agentId", int.Parse(house.Agent.ID));
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Connection.Close();
                GuiState.RaiseEstatesChanged();
                return true;
            }
            catch (Exception xcpt)
            {
                System.Windows.MessageBox.Show(xcpt.Message);
                return false;
            }
        }

        public bool DeleteHouse(House house)
        {
            try
            {
                if (Connection.State != System.Data.ConnectionState.Open)
                    Connection.Open();
                var query = $"delete from vsisp38.house where id=@id";
                var cmd = new NpgsqlCommand(query, Connection);
                cmd.Parameters.AddWithValue("id", house.ID);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Connection.Close();
                GuiState.RaiseEstatesChanged();
                return true;
            }
            catch (Exception xcpt)
            {
                System.Windows.MessageBox.Show(xcpt.Message);
                return false;
            }
        }

        public bool UpdateHouse(House house, string key)
        {
            try
            {
                if (Connection.State != System.Data.ConnectionState.Open)
                    Connection.Open();

                var query = $"update vsisp38.house set city = @city, postal_code = @postal_code, street = @street, street_no = @street_no, square_area = @square_area, floors = @floors, price = @price, garden = @garden, agentId = @agentId where id = @id";
                var cmd = new NpgsqlCommand(query, Connection);
                cmd.Parameters.AddWithValue("city", house.City);
                cmd.Parameters.AddWithValue("postal_code", house.PostalCode);
                cmd.Parameters.AddWithValue("street", house.Street);
                cmd.Parameters.AddWithValue("street_no", house.StreetNumber);
                cmd.Parameters.AddWithValue("square_area", house.SquareArea);
                cmd.Parameters.AddWithValue("floors", house.Floors);
                cmd.Parameters.AddWithValue("price", house.Price);
                cmd.Parameters.AddWithValue("garden", house.Garden);
                cmd.Parameters.AddWithValue("agentId", int.Parse(house.Agent.ID));
                cmd.Parameters.AddWithValue("id", int.Parse(key));
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Connection.Close();
                GuiState.RaiseEstatesChanged();
                return true;
            }
            catch (Exception xcpt)
            {
                System.Windows.MessageBox.Show(xcpt.Message);
                return false;
            }
        }


        #endregion

        #region Contracts

        public List<IContract> FetchContracts()
        {
            var purchase = new List<PurchaseContract>();
            var tenancy = new List<TenancyContract>();

            var query = "SELECT * FROM vsisp38.purchasecontract JOIN vsisp38.person ON vsisp38.purchasecontract.personId = vsisp38.person.id JOIN vsisp38.house ON vsisp38.purchasecontract.houseId = vsisp38.house.id";
            if (Connection.State != System.Data.ConnectionState.Open)
                Connection.Open();
            var cmd = new NpgsqlCommand(query, Connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var p = new PurchaseContract();
                p.ContractNo = reader.GetInt16(0).ToString();
                p.Date = reader.GetDateTime(1);
                p.Place = reader.GetString(2);
                p.Installments = reader.GetInt16(3);
                p.InterestRate = reader.GetDouble(4);
                p.Person = new Person { ID = reader.GetInt16(7).ToString(), LastName = reader.GetString(8), FirstName = reader.GetString(9), Address = reader.GetString(10) };
                p.Estate = new House { ID = reader.GetInt16(11).ToString(), City = reader.GetString(12), PostalCode = reader.GetString(13), Street = reader.GetString(14), StreetNumber = reader.GetString(15) };
                purchase.Add(p);

            }
            Connection.Close();

            var query2 = "SELECT * FROM vsisp38.tenancycontract JOIN vsisp38.person ON vsisp38.tenancycontract.personId = vsisp38.person.id JOIN vsisp38.apartment ON vsisp38.tenancycontract.apartmentId = vsisp38.apartment.id";

            if (Connection.State != System.Data.ConnectionState.Open)
                Connection.Open();
            var cmd2 = new NpgsqlCommand(query2, Connection);
            var reader2 = cmd2.ExecuteReader();
            while (reader2.Read())
            {
                var t = new TenancyContract();
                t.ContractNo = reader2.GetInt16(0).ToString();
                t.Date = reader2.GetDateTime(1);
                t.Place = reader2.GetString(2);
                t.StartDate = reader2.GetDateTime(3);
                t.Duration = reader2.GetInt16(4);
                t.AdditionalCosts = reader2.GetDouble(5);
                t.Person = new Person { ID = reader2.GetInt16(8).ToString(), LastName = reader2.GetString(9), FirstName = reader2.GetString(10), Address = reader2.GetString(11) };
                t.Estate = new Apartment { ID = reader2.GetInt16(12).ToString(), City = reader2.GetString(13), PostalCode = reader2.GetString(14), Street = reader2.GetString(15), StreetNumber = reader2.GetString(16) };
                tenancy.Add(t);
            }
            Connection.Close();

            var contracts = new List<IContract>();
            contracts.AddRange(purchase);
            contracts.AddRange(tenancy);
            return contracts.OrderBy(e => e.ContractNo).ToList();
        }

        public bool CreateTenancyContract(TenancyContract contract)
        {
            try
            {
                if (Connection.State != System.Data.ConnectionState.Open)
                    Connection.Open();

                var query = $"insert into vsisp38.tenancyContract (placementdate, place, startdate, duration, additional_costs, personid, apartmentid) VALUES(@placementdate, @place, @startdate, @duration, @additional_costs, @personid, @apartmentid)";
                var cmd = new NpgsqlCommand(query, Connection);
                cmd.Parameters.AddWithValue("placementdate", contract.Date);
                cmd.Parameters.AddWithValue("place", contract.Place);
                cmd.Parameters.AddWithValue("startdate", contract.StartDate);
                cmd.Parameters.AddWithValue("duration", contract.Duration);
                cmd.Parameters.AddWithValue("additional_costs", contract.AdditionalCosts);
                cmd.Parameters.AddWithValue("personid", int.Parse(contract.Person.ID));
                cmd.Parameters.AddWithValue("apartmentid", int.Parse(contract.Estate.ID));
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Connection.Close();
                GuiState.RaiseContractsChanged();
                return true;
            }
            catch (Exception xcpt)
            {
                System.Windows.MessageBox.Show(xcpt.Message);
                return false;
            }
        }


        public bool CreatePurchaseContract(PurchaseContract contract)
        {
            try
            {
                if (Connection.State != System.Data.ConnectionState.Open)
                    Connection.Open();

                var query = $"insert into vsisp38.purchasecontract (placementdate, place, installments, interest_rate, personid, houseid) VALUES(@placementdate, @place, @installments, @interest_rate, @personid, @houseid)";
                var cmd = new NpgsqlCommand(query, Connection);
                cmd.Parameters.AddWithValue("placementdate", contract.Date);
                cmd.Parameters.AddWithValue("place", contract.Place);
                cmd.Parameters.AddWithValue("installments", contract.Installments);
                cmd.Parameters.AddWithValue("interest_rate", contract.InterestRate);
                cmd.Parameters.AddWithValue("personid", int.Parse(contract.Person.ID));
                cmd.Parameters.AddWithValue("houseid", int.Parse(contract.Estate.ID));
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Connection.Close();
                GuiState.RaiseContractsChanged();
                return true;
            }
            catch (Exception xcpt)
            {
                System.Windows.MessageBox.Show(xcpt.Message);
                return false;
            }
        }

        #endregion
    }
}
