using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ContacList.Controllers
{

    public class ContactItem
    {
        public String firstname { get; set; }
        public String lastname { get; set; }
        public String email { get; set; }
    }
    [ApiController]
    [Route("/api")]
    public partial class ToDoController : ControllerBase
    {
        private static readonly List<ContactItem> items =
            new List<ContactItem> {
                new ContactItem{firstname="Hans",lastname="Peter",email="hans.peter@gmail.com"},
                new ContactItem{firstname="Amk",lastname="Peter",email="hans.peter@gmail.com"}
            };

        [HttpGet]
        [Route("contacts")]
        public IActionResult GetAllContacts()
        {
            return Ok(items);
        }

        [HttpPost]
        [Route("contacts")]
        public IActionResult AddContact([FromBody] ContactItem newItem)
        {
            items.Add(newItem);
            return CreatedAtRoute("GetSpecificItem", new { index = items.IndexOf(newItem) }, newItem);
        }

        [HttpDelete]
        [Route("contacts/{index}")]
        public IActionResult DeleteContact(int index)
        {
            if (index >= 0 && index < items.Count)
            {
                items.RemoveAt(index);
                return NoContent();
            }

            return BadRequest("Invalid index");
        }
        [HttpGet]
        [Route("contacts/findbyName")]
        public IActionResult GetByName([FromQuery]string nameFilter)
        {
            for(int i = 0; i < items.Count; i++)
            {
                if (items[i].firstname.Equals(nameFilter))
                {
                    return Ok(items[i]);
                }
            }
            return BadRequest("Invalid or missing sortOrder query parameter");
            
        }
    }
}
