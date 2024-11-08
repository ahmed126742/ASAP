using ASAP.Application.Features.Attachments.UpdateAttachment;
using CarMaintenance.Application.Features.AttachmentFeatures.GetAttachmentById;
using CarMaintenance.Application.Features.Attachments.AddAttachment;
using CarMaintenance.Application.Features.Attachments.DeleteAttachment;
using CarMaintenance.Application.Features.Attachments.GetAttachmentsByHeaderId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ASAP_Task.WebAPI.Controllers.Attachments
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentsController : Controller
    {

        private readonly IMediator _mediator;

        public AttachmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost("add-attachment")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<AddAttachmentResponse>> AddAttachment([FromForm] AddAttachmentRequest request, IFormFile file)
        {
            //-----Validation
            if (file is null)
                return BadRequest("File is required.");

            var isFileValid = ValidateFile(file);

            if (!isFileValid)
                return BadRequest("Invalid file. Please upload a file with a valid extension and size.");
            
            //-----

            var (relativePath, absolutePath) = ServerSaveFile("upload", file);

            var result = new AddAttachmentRequest(
                request.AttachmentHeaderId,
                Path.GetFileNameWithoutExtension(file.FileName),
                Path.GetFileNameWithoutExtension(file.FileName),
                request.Description,
                request.DescriptionEn,
                relativePath,
                file.ContentType,
                Path.GetExtension(file.FileName).ToLower(),
                file.Length.ToString(),
                request.UserId
             );

            return await _mediator.Send(result);
        }

        //[Authorize]
        [HttpGet]
        [Route("open-attachment/{id}")]
        public async Task<IActionResult> Download(Guid id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAttachmentByIdRequest(id) { }, cancellationToken);

            if (response is null || string.IsNullOrEmpty(response.Path))
                return NotFound();

            if (!System.IO.File.Exists(response.Path))
                return NotFound();

            var path = Path.Combine(Directory.GetCurrentDirectory(), response.Path);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, response.ContentType, response.Name + response.Extension);
        }

        [Authorize]
        [HttpPost("update-attachment")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<UpdateAttachmentResponse>> UpdateAttachment([FromForm] UpdateAttachmentRequest request, IFormFile file)
        {
            //-----Validation
            if (file is null)
                return BadRequest("File is required.");

            var isFileValid = ValidateFile(file);

            if (!isFileValid)
                return BadRequest("Invalid file. Please upload a file with a valid extension and size.");

            //-----

            var (relativePath, absolutePath) = ServerSaveFile("upload", file);

            var result = new UpdateAttachmentRequest(
                request.AttachmentId,
                request.AttachmentHeaderId,
                Path.GetFileNameWithoutExtension(file.FileName),
                Path.GetFileNameWithoutExtension(file.FileName),
                request.Description,
                request.DescriptionEn,
                relativePath,
                file.ContentType,
                Path.GetExtension(file.FileName).ToLower(),
                file.Length.ToString(),
                request.UserId
             );

            return await _mediator.Send(result);
        }

        [Authorize]
        [HttpPost]
        [Route("delete-attachment")]
        public async Task<ActionResult<DeleteAttachmentResponse>> Delete(DeleteAttachmentRequest request,
      CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        [Route("get-attachments-by-header-id")]
        public async Task<ActionResult<List<GetAttachmentsByHeaderIdResponse>>> GetAllAttachmentsByHeaderId(GetAttachmentsByHeaderIdRequest request,
      CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }


        private static bool ValidateFile(IFormFile file)
        {
            const int MAX_FILE_SIZE_IN_BYTES = 10485760; // 10MB

            //if (string.IsNullOrEmpty(file?.FileName))
            //    return false;

            if (file?.Length > 0 && file?.Length > MAX_FILE_SIZE_IN_BYTES)
                return false;

            if (GetAllowedMimeTypes().Any(x => x == file?.ContentType.ToLower()) == false)
                return false;

            if (GetAllowedFileExtensions().Any(x => x == Path.GetExtension(file?.FileName).ToLower()) == false)
                return false;

            return true;
        }

        private static string[] GetAllowedMimeTypes()
        {
            return new[]
            {
                "text/plain",
                "application/pdf",
                "application/vnd.ms-word",
                "application/vnd.ms-word",
                "application/vnd.ms-excel",
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "image/png",
                "image/jpeg",
                "image/jpeg",
                "image/gif",
                "text/csv",
                "audio/mpeg",
                "application/octet-stream" // ref-> https://stackoverflow.com/a/20509354, generic MIME type catch-all for any binary data that does not fit into a specific category for file download. it may be necessary to allow 'application/octet-stream' for handling certain types of binary data where the format is unknown or variable.
            };
        }

        private static string[] GetAllowedFileExtensions()
        {
            return new[]
           {
                ".txt",
                ".pdf",
                ".doc",
                ".docx",
                ".xls",
                ".xlsx",
                ".png",
                ".jpg",
                ".jpeg",
                ".gif",
                ".csv",
                ".mp3",
            };
        }

        private static (string relativeFilePath, string absoluteFilePath) ServerSaveFile(string dir, IFormFile file)
        {
            var relativeFilePath = Path.Combine(dir, $"{Guid.NewGuid()}_{DateTime.Now.ToString("yyyyMMddHHmmssffff")}_{file.FileName}");

            var ensureDirectory = Path.Combine(Directory.GetCurrentDirectory(), dir);
            if (!Directory.Exists(ensureDirectory))
                Directory.CreateDirectory(ensureDirectory);

            var absoluteFilePath = Path.Combine(Directory.GetCurrentDirectory(), relativeFilePath);
            using (Stream fileStream = new FileStream(absoluteFilePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return (relativeFilePath, absoluteFilePath);
        }
    }
}
