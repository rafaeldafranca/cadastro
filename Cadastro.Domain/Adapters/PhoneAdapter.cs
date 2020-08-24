using Cadastro.Domain.Entities;
using Cadastro.Domain.VO;

namespace Cadastro.Domain.Adapters
{
    public static class PhoneAdapter
    {
        public static Phone Adapter(this PhoneUserModel data)
        {
            if (data == null) return null;
            return new Phone()
            {
                Ddd = data.Ddd,
                Number = data.Number
            };
        }
    }
}
