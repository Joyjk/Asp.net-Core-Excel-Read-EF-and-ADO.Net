using System;

namespace TryFirstWorkApi.Testing
{
    public class TransferService
    {
        private readonly IValidateWireTransfer validateWireTransfer;

        public TransferService(IValidateWireTransfer validateWireTransfer)
        {
            this.validateWireTransfer = validateWireTransfer;
        }


        public void WireTransfer(Account origin, Account destination, decimal amount)
        {
            
            var state =  validateWireTransfer.validate(origin, destination, amount);    
            
            if(!state.IsSuccessful)
            {
                throw new ApplicationException(state.ErrorMessage);
            }

            //if(amount>origin.Funds)
            //{
            //    throw new ApplicationException("The account does not have enough ammount of money");
            //}
            origin.Funds -= amount;
            destination.Funds += amount;

        }

    }
}
