using LilFamStudioNet5.Data;
using LilFamStudioNet5.Facades;
using LilFamStudioNet5.ViewModels;
using LilFamStudioNet5.ViewModels.Discount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Controllers
{
    [Authorize(AuthenticationSchemes = "AdminCookie")]
    public class DiscountController : Controller
    {
        private ApplicationDbContext _dbc;
        public DiscountController(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        [HttpGet]
        [Route("/studio/discount/edit/{id_of_discount}")]
        public async Task<IActionResult> Edit(int id_of_discount)
        {
            DiscountFacade discountFacade = new DiscountFacade(_dbc);
            DiscountEditViewModel discountEditViewModel = await discountFacade.getEdit(id_of_discount);
            if (discountEditViewModel == null) return Redirect("/studio/discounts");
            return View(new JsonAnswerStatus("success", null, discountEditViewModel));
        }
    }
}
