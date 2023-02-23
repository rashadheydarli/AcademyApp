using System;
using Core.Entities;
namespace Data.Repositories.Abstract
{
	public interface IGroupRepository:IRepository<Group>
	{
        Group  GetByName(string name);
        List<Group> GetGroupsByStudentCount(int studentCount);

        // ancaq studente aid spesifik metodlari burda saxlayacam
    }
}

