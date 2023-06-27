using Domain;
using Domain.File;
using MassTransit;

namespace Application.Features.AddFile;

public class AddFileCommandHandler : IConsumer<AddFileCommand>
{
    private readonly IFileRepository _fileRepo;

    public AddFileCommandHandler(IFileRepository fileRepo)
    {
        _fileRepo = fileRepo;
    }

    public async Task Consume(ConsumeContext<AddFileCommand> context)
    {
        var fileToInsert = new BasicFileData
        {
            Id = Guid.NewGuid().ToString(),
            FileId = context.Message.FileId,
            Name = context.Message.Name
        };

        await _fileRepo.InsertFile(fileToInsert);
    }
}