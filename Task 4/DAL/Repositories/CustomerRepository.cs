﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_4.DAL.Models;
using Task_4.Models;

namespace Task_4.DAL.Repositories
{
    public class CustomerRepository : Repository<CustomerDAL>
    {
        private UsersContext context;
        private DbSet<CustomerDAL> dbSet;

        public CustomerRepository(UsersContext context) : base(context)
        {
            this.context = context;
            this.dbSet = context.Set<CustomerDAL>();
        }

        public override int? GetId(CustomerDAL item)
        {
            var tmp = context.Customers.FirstOrDefault(x => (x.CustomerName == item.CustomerName));
            if (tmp == null)
            {
                return null;
            }
            else
            {
                return tmp.CustomerId;
            }
        }

        public Customer ToEntity(CustomerDAL source)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CustomerDAL, Customer>()).CreateMapper();
            return mapper.Map<CustomerDAL, Customer>(source);
        }

        public CustomerDAL ToObject(Customer source)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Customer, CustomerDAL>()).CreateMapper();
            return mapper.Map<Customer, CustomerDAL>(source);
        }

        public new void Insert(CustomerDAL item)
        {
            context.Customers.Add(ToEntity(item));
        }

        public new IEnumerable<CustomerDAL> GetAll()
        {
            return context.Customers.Select(x => new CustomerDAL() { Id = x.CustomerId, CustomerName = x.CustomerName }).ToArray();
        }

        public new CustomerDAL GetById(int Id)
        {
            return ToObject(context.Customers.FirstOrDefault(x => (x.CustomerId == Id)));
        }

        public void Update(Customer item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
