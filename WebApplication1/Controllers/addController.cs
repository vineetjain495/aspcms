using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [RoutePrefix("Api/Employee")]
    public class addController : ApiController
    {
        RMSAutomation_UATEntities2 objEntity = new RMSAutomation_UATEntities2();
        DateTime now = DateTime.Now;
        string EPassword = "cms.info@2020";
        [HttpGet]
        [Route("AllEmployeeDetails")]
        public IQueryable<employee_role> GetEmaployee()
        {
            try
            {
                return objEntity.employee_role.Where(e => e.Type == "Employees");
            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }
        [HttpPost]
        [Route("PostNewEmployee")]
        //Get action methods of the previous section
        public IHttpActionResult PostNewEmployee(employee_role emp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            DateTime now = DateTime.Now;
            using (var ctx = new RMSAutomation_UATEntities2())
            {
                 ctx.employee_role.Add(new employee_role()
                {
                    Type_EmpCode = emp.Type_EmpCode,
                    EmployeeName = emp.EmployeeName,
                    MobileNumber = emp.MobileNumber,
                    Type = "Employees",
                    Password = EPassword,
                    EmailId = emp.EmailId,
                    RoleCode = emp.RoleCode,
                    RightsCode = emp.RightsCode,
                    CreatedDate = now,
                    FromDate = now,
                    IsActive = true,
                    MspCategory = emp.MspCategory

                });

                ctx.SaveChanges();

                /*atmmaster amm = objEntity.atmmasters.Where(e => e.HubLocationCode);*/
                var result = objEntity.atmmasters.Select(m => new
                    {
                        m.RoCode,
                        m.RoName,
                        m.LocationCode,
                        m.LocationName,
                        m.HubLocationCode,
                        m.HubLocationName
                    }).Distinct().Where(i => emp.Hub.Contains(i.HubLocationCode)).ToList();

                foreach (var value in result)
                {
                    Console.WriteLine(value);
                    ctx.employee_hierarchy.Add(new employee_hierarchy()
                    {
                        EmployeeCode = emp.Type_EmpCode,
                        Region_Code = value.RoCode,
                        Loc_Code = value.LocationCode,
                        Hub_Location_Code = value.HubLocationCode,
                        CreatedDate = now,
                        FromDate = now,
                        IsActive = 1
                       

                    });
                    ctx.SaveChanges();

                }

            }

            return Ok();
        }
        //Get action methods of the previous section
        [HttpGet]
        [Route("GetNewemp")]
        public IHttpActionResult GetNewemp(bool getraw = false)
        {
           

            using (var ctx = new RMSAutomation_UATEntities())
            {

              var get_detail = ctx.employee_role.SqlQuery("select e.*, m.Type_EmpCode as RoleName, n.Type_EmpCode as Rightname  from employee_role e Left join employee_role m on m.Type='Roles' and e.RoleCode = m.TypeCode Left join employee_role n on n.Type='Rights' and e.RightsCode = n.TypeCode where e.Type = 'Employees'").FirstOrDefault();

                return Ok(get_detail) ;
            }

            
        }
        [HttpGet]
        [Route("AllEmployeelimited")]
        public IHttpActionResult GetEmaployeelimited()
        {
            try
            {
                var query = (from RR in objEntity.employee_role
                                 from M in objEntity.employee_role
                                 where RR.RoleCode == M.TypeCode
                                       && M.Type == "Roles"
                                 from N in objEntity.employee_role
                                 where RR.RoleCode == N.TypeCode
                                       && N.Type == "Rights"

                             where RR.Type == "Employees"
                             select new
                             {  
                                 ID = RR.ID,
                                 Type_EmpCode = RR.Type_EmpCode,
                                 EmployeeName = RR.EmployeeName,
                                 MobileNumber = RR.MobileNumber,
                                 Password = RR.Password,
                                 EmailID = RR.EmailId,
                                 RoleCode = RR.RoleCode,
                                 RightsCode = RR.RightsCode,

                                 RoleName = M.Type_EmpCode,
                                 RightsName = N.Type_EmpCode,
                                 
                             }
                    ).ToList();

               /* var query = from RR in objEntity.employee_role
                            where RR.Type == "Employees"
                            join p in objEntity.employee_role on RR.RoleCode equals p.TypeCode
                            select new { RR, RoleName = p.Type_EmpCode };*/
                return Ok(query);
            }
            catch (Exception)   
            {
                throw;
            }
        }
        [HttpGet]
        [Route("GetLocatioDetail")]
        public IHttpActionResult GetLocatioDetail()
        {
            try
            {
                var result = objEntity.atmmasters.Select(m=> new
                {
                    m.RoCode,
                    m.RoName,
                    m.LocationCode,
                    m.LocationName,
                    m.HubLocationCode,
                    m.HubLocationName
                }).Distinct().OrderBy(e=> new { e.RoName, e.LocationCode, e.HubLocationCode });

                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetEmployeeDetailsById/{employeeId}")]
        public IHttpActionResult GetEmaployeeById(string employeeId)
        {
            employee_role objEmp = new employee_role();
            /*int ID = Convert.ToInt32(employeeId);*/
            
            try
            {
                objEmp = objEntity.employee_role.Where(e => e.Type_EmpCode == employeeId).FirstOrDefault();
                /*var studentName = objEntity.employee_role.ToList();*/
                /*if (objEmp == null)
                {
                    return Ok(;
                }*/
                /*return Ok(studentName);*/
            }
            catch (Exception)
            {
                return NotFound();
               /* throw;*/

            }

            return Ok(objEmp);
        }

        [HttpPost]
        [Route("InsertEmployeeDetails")]
        public IHttpActionResult PostEmaployee(employee_role data)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                objEntity.employee_role.Add(data);
                objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(data);
        }

        [HttpPut]
        [Route("UpdateEmployeeDetails")]
        public IHttpActionResult PutEmaployeeMaster(employee_role employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //employee_role objEmp = new employee_role();
                var objEmp = objEntity.employee_role.Where(e => e.Type_EmpCode == employee.Type_EmpCode).FirstOrDefault<employee_role>();
                if (objEmp != null)
                {
                    objEmp.EmployeeName = employee.EmployeeName;
                    objEmp.MobileNumber = employee.MobileNumber;
                    objEmp.EmailId = employee.EmailId;
                    objEmp.Password = employee.Password;
                    objEmp.RoleCode = employee.RoleCode;
                    objEmp.RightsCode = employee.RightsCode;
                    objEmp.ModifiedDate = now;

                }
                int i = this.objEntity.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
            return Ok(employee);
        }
        [HttpDelete]
        [Route("DeleteEmployeeDetails")]
        public IHttpActionResult DeleteEmaployeeDelete(string emp_code)
        {
            //int empId = Convert.ToInt32(id);  
            employee_role emaployee = objEntity.employee_role.Where(e => e.Type_EmpCode == emp_code).FirstOrDefault<employee_role>();
            if (emaployee == null)
            {
                return NotFound();
            }

            objEntity.employee_role.Remove(emaployee);
            objEntity.SaveChanges();

            return Ok(emaployee);
        }
    }
}

/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class addController : ApiController
    {
    }
*/

