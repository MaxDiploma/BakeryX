using Bakeshop.CommandHandler;
using Bakeshop.Common.Enums;
using Bakeshop.EF;
using Bakeshop.StaticResources;
using System;
using System.Linq;
using System.Windows.Input;

namespace Bakeshop.DomainModels
{
    public class EmployeeDomain
    {
        private ICommand _removeUserCommand;
        private ICommand _editUserCommand;
        private BakeshopContext _context;
        public EventHandler<EventArgs> OnEmployeeRemoved;

        public EmployeeDomain()
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

        public string Password { get; set; }

        public bool IsOwnerOrAdmin
        {
            get
            {
                var user = CurrentUserManagment.GetCurrentUser();
                return user.Position == Positions.Manager || user.Position == Positions.Owner ? true : false;
            }
        }

        public ICommand RemoveUserCommand
        {
            get
            {
                return _removeUserCommand ?? (_removeUserCommand = new BaseCommandHandler(param => RemoveUser(param), true));
            }
        }

        public ICommand EditUserCommand
        {
            get
            {
                return _editUserCommand ?? (_editUserCommand = new BaseCommandHandler(param => EditUser(param), true));
            }
        }

        private void RemoveUser(object param)
        {
            var id = param as Guid?;

            var employee = _context.BakeshopWorkers.FirstOrDefault(bw => bw.Id == id);

            _context.BakeshopWorkers.Remove(employee);
            _context.SaveChanges();

            var handler = OnEmployeeRemoved;

            OnEmployeeRemoved?.Invoke(this, null);
        }

        private void EditUser(object param)
        {
            var id = param as Guid?;

            var employee = _context.BakeshopWorkers.FirstOrDefault(bw => bw.Id == id);

            _context.SaveChanges();

        }
    }
}
