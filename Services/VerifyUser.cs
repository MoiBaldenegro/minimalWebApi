using DPFP;

namespace WebMinimalApi.Services
{
    public class VerifyUser
    {
        private DPFP.Template _template;
        private DPFP.Verification.Verification _verificator;

        public VerifyUser()
        {
            _verificator = new DPFP.Verification.Verification(); // Inicializar el Verificator
        }

        public object Verify(DPFP.Template template)
        {
            _template = template;
            return new { message = "todo bien por aca" };
        }
    }
}
