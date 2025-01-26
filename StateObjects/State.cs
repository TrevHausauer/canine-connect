using CanineConnect.Models;

namespace CanineConnect.StateObjects
{
    public class State
    {
        private User? _activeUser;
        private Shelter? _activeShelter;

        public User? ActiveUser
        {
            get { return _activeUser; }
            set
            {
                _activeUser = value;
                Notify?.Invoke();
            }
        }
        public Shelter? ActiveShelter
        {
            get { return _activeShelter; }
            set 
            { 
                _activeShelter = value; 
                Notify?.Invoke(); 
            }
        }
        
        public bool IsUser()
        {
            return ActiveUser is not null;
        }

        public bool IsShelter()
        {
            return ActiveShelter is not null;
        }

        public bool IsAdmin()
        {
            if (!IsUser()) return false;

            return ActiveUser?.Email == "admin";
        }

        public event Action Notify;
    }
}
