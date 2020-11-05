using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Permissions;
using System.Web.Http;
using Mvccrudf.Models;


namespace Mvccrudf.Controllers
{
    public class RegionCrudController : ApiController
    {
        iSurveyEntities it = new iSurveyEntities();
        public Guid RowNumber;
        public IHttpActionResult getrec()
        {
            var results = it.RegionLocationUnitsAlls.ToList();
            return Ok(results);
        }

        public IHttpActionResult reginsert(RegionLocationUnitsAll reginsert)
        {
            
            it.RegionLocationUnitsAlls.Add(reginsert);
            it.SaveChanges();
            return Ok();
        }

        public IHttpActionResult GetRowNumber(Guid id)
        {
        RegClass regdetails = null;
        regdetails = it.RegionLocationUnitsAlls.Where(x => x.RowNumber == id).Select(x => new RegClass()
        {
        RowNumber = x.RowNumber,
        Region = x.Region,
        Location = x.Location,
        Unit = x.Unit,
        }).FirstOrDefault<RegClass>();
        if (regdetails == null)
        {
                return NotFound();
        }
        return Ok(regdetails);
        }
        public IHttpActionResult Put(RegClass rc)
        {
            var updatereg = it.RegionLocationUnitsAlls.Where(x => x.RowNumber == rc.RowNumber).FirstOrDefault<RegionLocationUnitsAll>();
            if (updatereg != null)
            {
                updatereg.RowNumber = rc.RowNumber;
                updatereg.Region = rc.Region;
                updatereg.Location = rc.Location;
                updatereg.Unit = rc.Unit;
                it.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }
        public IHttpActionResult Delete(Guid id)
        {
            var regdel = it.RegionLocationUnitsAlls.Where(x => x.RowNumber == id).FirstOrDefault();
            it.Entry(regdel).State = System.Data.Entity.EntityState.Deleted;
            it.SaveChanges();
            return Ok();
        }
    }
}
