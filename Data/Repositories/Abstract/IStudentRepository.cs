using System;
using Core.Entities;

namespace Data.Repositories.Abstract
{
	public interface IStudentRepository: IRepository<Student>
	{
        public bool IsDuplicateEmail(string email);
        // ancaq studente aid spesifik metodlari burda saxlayacam
    }
}

