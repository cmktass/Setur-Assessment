
using ContactService.Api.Requests;
using ContactService.Application.Commands;
using ContactService.Application.Dtos;
using ContactService.Application.Queries;
using Core.Web;
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
            return Ok(new ApiResponse<List<ContactDto>>().Success(contacts));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDto>> GetContactWithInfos([FromRoute] Guid id)
        {
            var contacts = await _mediator.Send(new GetContactWithInfosQuery(id));
            return Ok(new ApiResponse<ContactDto>().Success(contacts));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDto>> GetContact([FromRoute] Guid id)
        {
            var contact = await _mediator.Send(new GetContactByIdQuery(id));
            return Ok(new ApiResponse<ContactDto>().Success(contact));
        }

        [HttpPost]
        public async Task<ActionResult> CreateContact([FromBody] CreateContactRequest request)
        {
            var result = await _mediator.Send(_mapper.Map<CreateContactCommand>(request));
            return Ok(new ApiResponse<Guid>().Success(result, 200));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteContact([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteContactCommand(id));
            return Ok(new ApiResponse<object>().Success(204));
        }

        [HttpPost]
        public async Task<ActionResult> AddContactInfo([FromBody] CreateContactInfoRequest contactInfoDto)
        {
            var result = await _mediator.Send(_mapper.Map<CreateContactInfoCommand>(contactInfoDto));
            return Ok(new ApiResponse<Guid>().Success(result, 200));
        }

        [HttpDelete("{id}/info/{infoId}")]
        public async Task<ActionResult> RemoveContactInfo([FromRoute] Guid id, int infoId)
        {
            await _mediator.Send(new RemoveContactInfoCommand(id, infoId));
            return Ok(new ApiResponse<object>().Success(200));
        }

        [HttpPost]
        public async Task<ActionResult> PerepareContactInfoReport()
        {
            var message = await _mediator.Send(new PrepareContactInfoReportCommand());
            return Ok(new ApiResponse<string>().Success(message));
        }
    }
}

