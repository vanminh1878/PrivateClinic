using PrivateClinic.ViewModel.OtherViewModels;

namespace PrivateClinic.Model
{
    public class Const : BaseViewModel
    {

        public static string _localLink = System.Reflection.Assembly.GetExecutingAssembly().Location.Remove(System.Reflection.Assembly.GetExecutingAssembly().Location.IndexOf(@"bin\Debug"));
    }
}
