using System;

namespace TryFirstWorkApi.Testing
{
    public class WireTransferValidator : IValidateWireTransfer
    {
        public OperationResult validate(Account origin, Account destination, decimal amount)
        {
            if (amount > origin.Funds)
            {
                throw new ApplicationException("The account does not have enough ammount of money");
            }
            // other validation

            return new OperationResult(true);

        }
    }
}
