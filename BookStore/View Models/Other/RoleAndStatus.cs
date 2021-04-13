using System;
namespace BookStore.ViewModels.Other
{
    public class RoleAndStatus
    {
        public RoleAndStatus()
        {
            
        }
        public int ?Id { get; set; }
        public int ?RoleId { get; set; }
        public bool ?IsAccess { get; set; }
    }
}
