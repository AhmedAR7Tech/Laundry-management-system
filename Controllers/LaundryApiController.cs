using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClsBuisnesslayer;
using ClsDataAccess;
using Models;
namespace ProjectLaundryForApis.Controllers
{
    [Route("api/Laundry")]
    [ApiController]
    public class LaundryApiController : ControllerBase
    {
        [HttpPost("AddNewClient")]
        public ActionResult<ClsModel.PersonInfoForSign> AddNewClient(ClsModel.PersonInfoForSign Person)
        {
            try
            {
                bool Success = ClsBuisnesslayer.ClsBuisnessLayer.Signin(Person);

                ClsModel.PersonInfo PersonPlus = new ClsModel.PersonInfo()
                {
                    Id = Person.Id,
                    Name = Person.Name,
                    Address = Person.Address,
                    PhoneNumber = Person.PhoneNumber,
                    Role = Person.Role,
                };

                if (Success)
                {
                    return Created("", PersonPlus);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Could not create client");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public ActionResult<ClsModel.PersonInfo> Login(ClsModel.PersonInfoLogin person)
        {
            int PersonID = ClsBuisnessLayer.Login(person);

            if (PersonID > 0)
            {
                var PersonInfo = ClsBuisnesslayer.ClsBuisnessLayer.GetClientByID(PersonID);
                return Ok(PersonInfo);
            }

            return Unauthorized("Invalid email or password");
        }

        [HttpPost("AddServices")]
        public ActionResult<ClsModel.AddServices> AddServices(ClsModel.AddServices Services)
        {
            var Service = ClsBuisnessLayer.AddServicesPlus(Services);

            if(Services == null)
            {
                return NotFound("فشل ف اضافه الخدمه");
            }
            return Ok(Service);
        }

        [HttpGet("GetAllServices")]
        public ActionResult<IEnumerable<ClsModel.ServicesItems>> GetAllServices()
        {
            try
            {
                var services = ClsBuisnessLayer.GetAllServices();

                // حالة: حصل Null نتيجة عدم وجود بيانات
                if (services == null)
                {
                    return NotFound(new { message = "لم يتم العثور على خدمات." });
                }

                // حالة: رجع List فاضية
                if (!services.Any())
                {
                    return NoContent(); // Status code 204 بدون body
                }

                // حالة: نجاح وتم جلب البيانات
                return Ok(services);
            }
            catch (ArgumentException ex)
            {
                // حالة: إدخال غير صالح أو خطأ منطقي
                return BadRequest(new { message = "طلب غير صالح", details = ex.Message });
            }
            catch (Exception ex)
            {
                // حالة: خطأ داخلي غير متوقع
                return StatusCode(500, new { message = "حدث خطأ غير متوقع", details = ex.Message });
            }
        }

        [HttpPost("CreateOrder")]
        public ActionResult<IEnumerable<ClsModel.OrderRequestModel>> CreateOrders(ClsModel.OrderRequestModel Order)
        {
            object OrderPlus = ClsBuisnessLayer.SaveOrderWithDetails(Order);

            if (OrderPlus != null)
            {
                return Ok(Order);
            }

            return Unauthorized("Error Of Full Object");
        }

        [HttpGet("GetAllOrder")]
        public ActionResult<List<ClsModel.OrderDetailsWithCustomerModel>> GetAllOrder()
        {
            var orders = ClsBuisnesslayer.ClsBuisnessLayer.GetAllOrderDetailsWithCustomer();

            if (orders == null || !orders.Any())
            {
                return NotFound("لا يوجد طلبات حالياً");
            }

            return Ok(orders);
        }

        [HttpPut("UpdateStatus")]
        public ActionResult<ClsModel.StatusUpdateModel>UpdateStatus(ClsModel.StatusUpdateModel Status) 
        {
            if (Status == null || Status.OrderID <= 0)
            {
                return BadRequest("Invalid data.");
            }

            bool result = ClsBuisnesslayer.ClsBuisnessLayer.UpdateStatusOrder(Status);

            if (result)
            {
                return Ok(Status); // رجع البيانات بعد التعديل
            }
            else
            {
                return StatusCode(500, "Failed to update order status.");
            }
        }

        [HttpGet("GetOrderByID/{UserID}")]
        public ActionResult<ClsModel.GetOrderBYID>GetOrderBYID(int UserID)
        {
            var Order = ClsBuisnesslayer.ClsBuisnessLayer.GetOrderBYID(UserID);

            if (Order == null || !Order.Any())
            {
                return NotFound("لا يوجد طلبات حالياً");
            }

            return Ok(Order);
        }

        [HttpGet("GetAllPerson")]

        public ActionResult<List<ClsModel.PersonInfoForSign>> GetAllPerson()
        {
            var PersonInfo = ClsBuisnessLayer.GetAllPersons();

            if(PersonInfo != null)
            {
                return Ok(PersonInfo);
            }
            return NotFound("لا يوجد اشخاص لعرضهم");
        }

        [HttpDelete("DeleteServices/{ServicesID}")]
        public ActionResult<bool>DeleteServices(int ServicesID)
        {
            return ClsBuisnessLayer.DeleteServices(ServicesID);
        }
    }
}

