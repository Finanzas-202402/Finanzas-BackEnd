using Finanzas_BackEnd.IAM.Interfaces.ACL;

namespace Finanzas_BackEnd.Bills.Application.Internal.OutboundServices.ACL;

public class ExternalIamService(IIamContextFacade iamContextFacade)
{
    public async Task<int> FetchUserIdByEmail(string email)
    {
        return await iamContextFacade.FetchUserIdByEmail(email);
    }
}