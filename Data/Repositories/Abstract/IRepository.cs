using System;
using Core.Entities;

namespace Data.Repositories.Abstract
{                                                             //baseentityden toreyen bir class olmalidi
	public interface IRepository<T> where T: BaseEntity     //hansi classlarin repositoryleri olacaqsa
	{
        List<T> GetAll();
        T Get(int id);
        void Add(T item);
        void Update(T item);   // bunlar writedi / read olanlari yuxarida yaziriq
        void Delete(T item);


        // abstract metodlari ozunde saxladi
        //implement etmek ucun concrete bax
    }
}

