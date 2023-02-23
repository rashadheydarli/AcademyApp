using System;
using Core.Entities;

namespace Data.Repositories.Abstract
{
	public interface IAdminRepository   // admini add update delete etmediyimiz ucun IRepositoryden implement etmir(Solid interface segregation prinsipi)
	{
		Admin GetByUsernameAndPassword(string username, string password);
	}
}

