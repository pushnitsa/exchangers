﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiExchanger.Models;
using WebApiExchanger.DataObject;

namespace WebApiExchanger.Controllers
{
    [Authorize]
    public class ExchangersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Exchangers/1/2
        [ResponseType(typeof(ExchangerDataObject))]
        public IQueryable<ExchangerDataObject> GetExchangers(int from, int to)
        {
            IQueryable<ExchangerDataObject> obj = db.Exchangers
                .Where(c => c.CurrencyFrom.Id == from && c.CurrencyTo.Id == to)
                .Select(c => new ExchangerDataObject {
                    CurrencyFromName = c.CurrencyFrom.Name,
                    ExchangerName = c.Name,
                    CurrencyToName = c.CurrencyTo.Name
                })
                .AsQueryable();

            return obj;
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExchangerExists(int id)
        {
            return db.Exchangers.Count(e => e.Id == id) > 0;
        }
    }
}
