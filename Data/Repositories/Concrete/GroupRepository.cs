using System;
using Data.Contexts;
using Core.Entities;
using Data.Repositories.Abstract;

namespace Data.Repositories.Concrete
{
    public class GroupRepository : IGroupRepository
    {
        static int id;
        public List<Group> GetAll()
        {
           return DbContext.Groups;
        }

        public Group Get(int id)
        {
            return DbContext.Groups.FirstOrDefault(g => g.Id == id);
        }

        public void Add(Group group)
        {
            id++;
            group.Id = id;
            group.CreatedAt = DateTime.Now;
            DbContext.Groups.Add(group);   //static oldugu ucun obyekt yaratmadan birbasa muraciet edirik

        }

        public void Update(Group group)
        {
            
        }

        public void Delete(Group group)
        {
            DbContext.Groups.Remove(group);
        }
       


    }
}

