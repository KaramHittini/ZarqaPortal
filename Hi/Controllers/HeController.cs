using Hi.BridgeData;
using Hi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hi.Controllers
{
    public class HeController : Controller
    {
        private readonly Bridge _bridge;
        public HeController(Bridge b)
        {
            _bridge = b;
        }
        //-----------------------------------------------------

        public static List<Class> M = new List<Class>();
        
        //-----------------------------------------------------
        public async Task<IActionResult> Show()
        {
            var showList = await _bridge.Show.ToListAsync();
            if(RegistrationController.currentUsers == null)
            {
                return RedirectToAction("LogIn" , "Registration");
            }
            return View(showList);

        }
        //-----------------------------------------------------
        public IActionResult Detial(int Id)
        {
            var exixt = M.Find(x => x.Id == Id);
            return View(exixt);
        }
        //-----------------------------------------------------
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost] // اذا كانو نفس الاسم   add add
        public async Task<IActionResult> Add(Class p)
        {
            if (ModelState.IsValid)
            {
                await _bridge.Show.AddAsync(p);
                await _bridge.SaveChangesAsync();

                p.Id = M.Count > 0 ? M.Max(x => x.Id) + 1 : 1;
                M.Add(p);
                return RedirectToAction("Show");
            }
            return View(p);
        }
        //-----------------------------------------------------
        public IActionResult AddUser()
        {
            return View(M);
        }
        [HttpPost] // اذا كانو نفس الاسم   add add
        public IActionResult AddUser(Class p)
        {
            if (ModelState.IsValid)
            {
                p.Id = M.Count > 0 ? M.Max(x => x.Id) + 1 : 1;
                M.Add(p);
                return RedirectToAction("AddUser");
            }
            return View();
        }
        //-----------------------------------------------------
        public IActionResult Delete(int Id)
        {
            var exixt = M.Find(x => x.Id == Id);
            if (exixt == null)
            {
                return NotFound();
            }
            return View(exixt);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete2(int Id)
        {
            var exixt = M.Find(x => x.Id == Id);
            if (exixt == null)
            {
                return NotFound();
            }
            M.Remove(exixt);
            return RedirectToAction("Show");
        }
        //-----------------------------------------------------
        public IActionResult Edit(int Id)
        {
            var exixt = M.Find(x => x.Id == Id);
            if (exixt == null)
            {
                return NotFound();
            }
            return View(exixt);
        }
        [HttpPost]
        public IActionResult Edit(Class p)
        {
            if (ModelState.IsValid)
            {
                var old = M.Find(x => x.Id == p.Id);
                if (old == null)
                {
                    return NotFound();
                }
                old.Material = p.Material;
                old.DrName = p.DrName;
                return RedirectToAction("Show");
            }
            return View(p);
        }
        //-----------------------------------------------------
        public IActionResult UserTable()
        {
            return View(M);
        }
        //-----------------------------------------------------
        public IActionResult Profile() 
        {
            var s = new profile();
            return View(s);
        }
        
    }
}
