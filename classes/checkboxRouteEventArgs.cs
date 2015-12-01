using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectK.ErgoMC.Assessment.classes
{
   public class checkboxRouteEventArgs : System.Windows.RoutedEventArgs
    {
       public int parameterValue { get; set; }
       public checkboxRouteEventArgs(System.Windows.RoutedEvent routedEvent, int parameterValue)
       {

       }
    }
}
