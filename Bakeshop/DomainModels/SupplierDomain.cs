using Bakeshop.CommandHandler;
using Bakeshop.Common.Enums;
using Bakeshop.EF;
using Bakeshop.StaticResources;
using System;
using System.Linq;
using System.Windows.Input;

namespace Bakeshop.DomainModels
{
    public class SupplierDomain
    {
        public EventHandler<EventArgs> OnSupplierRemoved;
        public ICommand _removeSupplierCommand;
        private BakeshopContext _context;

        public SupplierDomain()
        {
            _context = new BakeshopContext();
        }

        public Guid Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }

        public Positions Position { get; set; }

        public string Phone { get; set; }

        public string Products { get; set; }

        public bool IsOwnerOrAdmin
        {
            get
            {
                var user = CurrentUserManagment.GetCurrentUser();
                return user.Position == Positions.Manager || user.Position == Positions.Owner ? true : false;
            }
        }

        public ICommand RemoveSupplierCommand
        {
            get
            {
                return _removeSupplierCommand ?? (_removeSupplierCommand = new BaseCommandHandler(param => RemoveSupplier(param), true));
            }
        }

        private void RemoveSupplier(object param)
        {
            var id = param as Guid?;

            var supplier = _context.Suppliers.FirstOrDefault(bw => bw.Id == id);

            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();

            var handler = OnSupplierRemoved;

            OnSupplierRemoved?.Invoke(this, null);
        }
    }
}
