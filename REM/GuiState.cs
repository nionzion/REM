using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REM
{
    public static class GuiState
    {
        public static event PropertyChangedEventHandler OnAgentsChanged = delegate { };
        static void OnAgentsPropertyChanged(string propertyName)
        {
            OnAgentsChanged(
                typeof(GuiState),
                new PropertyChangedEventArgs(propertyName));
        }
        private static List<EstateAgent> _agents = new List<EstateAgent>();

        public static void RaiseAgentsChanged()
        {
            OnAgentsPropertyChanged("Agents");
        }


        public static event PropertyChangedEventHandler OnEstatesChanged = delegate { };
        static void OnEstatesPropertyChanged(string propertyName)
        {
            OnEstatesChanged(
                typeof(GuiState),
                new PropertyChangedEventArgs(propertyName));
        }

        public static void RaiseEstatesChanged()
        {
            OnEstatesPropertyChanged("Estates");
        }



        public static event PropertyChangedEventHandler OnPersonsChanged = delegate { };
        static void OnPersonsPropertyChanged(string propertyName)
        {
            OnPersonsChanged(
                typeof(GuiState),
                new PropertyChangedEventArgs(propertyName));
        }
        public static void RaisePersonsChanged()
        {
            OnPersonsPropertyChanged("Persons");
        }
    }
}
