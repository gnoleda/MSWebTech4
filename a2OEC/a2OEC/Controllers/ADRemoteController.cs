using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using a2OEC.Models;

namespace a2OEC.Controllers
{
    public class ADRemoteController : Controller
    {
        private readonly OECContext _context;

        public ADRemoteController(OECContext context)
        {
            _context = context;
        }

        public JsonResult ProvinceCodeValidation(string ProvinceCode)
        {
            try
            {
                //it must be exactly 2 letters long (not just any characters)
                Regex pcPattern = new Regex(@"^[a-z]{2}$");
                if (!pcPattern.IsMatch(ProvinceCode))
                {
                    return Json("Province code should be two letters.");
                }
                if (pcPattern.IsMatch(ProvinceCode))
                {
                    //search database for the province code
                    var provinceCode = _context.Province.SingleOrDefault(p => p.ProvinceCode == ProvinceCode);

                    //it must be on file in the province table
                    if (provinceCode == null)
                    {
                        return Json("Province code does not exist.");
                    }
                }
            }
            catch (Exception ex)
            {

                return Json("Error validating province code.");
            }

            return Json(true);
        }
    }
}