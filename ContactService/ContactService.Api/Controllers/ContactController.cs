
using ContactService.Api.Requests;
using ContactService.Application.Commands;
using ContactService.Application.Dtos;
using ContactService.Application.Queries;
using ContactService.Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ContactController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public ContactController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDto>>> GetContacts()
        {
            var contacts = await _mediator.Send(new GetContactsQuery());
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ContactDto>>> GetContactWithInfos([FromRoute] Guid id)
        {
            var contacts = await _mediator.Send(new GetContactWithInfosQuery(id));
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDto>> GetContact([FromRoute] Guid id)
        {
            var contact = await _mediator.Send(new GetContactByIdQuery(id));
            if (contact == null) return NotFound();
            return Ok(contact);
        }

        [HttpPost]
        public async Task<ActionResult> CreateContact([FromBody] CreateContactRequest request)
        {
            return CreatedAtAction(nameof(GetContact), await _mediator.Send(_mapper.Map<CreateContactCommand>(request)));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteContact([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteContactCommand(id));
            if (result == 0) return NotFound();
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> AddContactInfo([FromBody] CreateContactInfoRequest contactInfoDto)
        {
            var contactInfo = await _mediator.Send(_mapper.Map<CreateContactInfoCommand>(contactInfoDto));
            if (contactInfo == null) return NotFound("Contact not found.");
            return Ok(contactInfo);
        }

        [HttpDelete("{id}/info/{infoId}")]
        public async Task<ActionResult> RemoveContactInfo([FromRoute] Guid id, int infoId)
        {
            await _mediator.Send(new RemoveContactInfoCommand(id, infoId));
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> PerepareContactInfoReport()
        {
            var message = await _mediator.Send(new PrepareContactInfoReportCommand());
            return Ok(message);
        }
    }
}

