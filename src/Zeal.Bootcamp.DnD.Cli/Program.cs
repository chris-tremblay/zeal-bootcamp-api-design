using McMaster.Extensions.CommandLineUtils;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Zeal.Bootcamp.DnD.Application;
using Zeal.Bootcamp.DnD.Application.Commands.CreateCharacter;
using Zeal.Bootcamp.DnD.Application.Data.Queries.ListCharacters;
using Zeal.Bootcamp.DnD.Application.Queries.ListCharacters;
using Zeal.Bootcamp.DnD.Data;
using Zeal.Bootcamp.DnD.Data.Extensions;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(new HostApplicationBuilderSettings
{
    Args = args,
    ContentRootPath = AppContext.BaseDirectory,
});

builder.Logging.SetMinimumLevel(LogLevel.Critical);
builder.Logging.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Critical);

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddMediatrBehaviors();
builder.Services.AddDataServices(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DnDDatabase")));

using IHost host = builder.Build();
using IServiceScope scope = host.Services.CreateScope();

IServiceProvider services = scope.ServiceProvider;
services.GetRequiredService<IDatabase>().Migrate();

var app = new CommandLineApplication
{
    Name = "dnd",
    Description = "Manage Zeal Bootcamp D&D characters.",
};

app.HelpOption(inherited: true);
app.OnExecute(() =>
{
    app.ShowHelp();
    return 0;
});

app.Command("create-character", command =>
{
    command.Description = "Create a character.";

    CommandOption name = command.Option<string>(
        "--name <NAME>", "The character name.", CommandOptionType.SingleValue)
        .IsRequired();
    CommandOption className = command.Option<string>(
        "--class <CLASS>", "The character class.", CommandOptionType.SingleValue)
        .IsRequired();
    CommandOption weapon = command.Option<string>(
        "--weapon <WEAPON>", "The character weapon.", CommandOptionType.SingleValue)
        .IsRequired();

    command.OnExecuteAsync(async cancellationToken =>
    {
        var request = new CreateCharacterCommand
        {
            Name = name.Value()!,
            ClassName = className.Value()!,
            Weapon = weapon.Value()!,
        };

        Guid characterId = await services.GetRequiredService<IMediator>()
            .Send(request, cancellationToken);

        Console.WriteLine($"Character created with Id: {characterId}");
        return 0;
    });
});

app.Command("list-characters", command =>
{
    command.Description = "List all characters.";
    command.OnExecuteAsync(async cancellationToken =>
    {
        IMediator mediator = services.GetRequiredService<IMediator>();
        IQueryable<ListCharactersDataQueryResult> query =
            await mediator.Send(new ListCharactersQuery(), cancellationToken);
        List<ListCharactersDataQueryResult> characters =
            await query.ToListAsync(cancellationToken);

        PrintTable(characters);
        return 0;
    });
});

return await app.ExecuteAsync(args);

static void PrintTable(IReadOnlyCollection<ListCharactersDataQueryResult> characters)
{
    string[] headers = ["Id", "Name", "Class", "Weapon"];
    string[][] rows = characters
        .Select(character => new[]
        {
            character.Id.ToString(),
            character.Name,
            character.ClassName,
            character.WeaponName,
        })
        .ToArray();

    int[] widths = headers
        .Select((header, column) => Math.Max(
            header.Length,
            rows.Select(row => row[column]?.Length ?? 0).DefaultIfEmpty().Max()))
        .ToArray();

    string border = "+-" + string.Join("-+-", widths.Select(width => new string('-', width))) + "-+";

    Console.WriteLine(border);
    PrintRow(headers, widths);
    Console.WriteLine(border);
    foreach (string[] row in rows)
    {
        PrintRow(row, widths);
    }
    Console.WriteLine(border);
    Console.WriteLine($"{characters.Count} character(s)");
}

static void PrintRow(IReadOnlyList<string> values, IReadOnlyList<int> widths)
{
    Console.WriteLine("| " + string.Join(" | ", values.Select(
        (value, column) => (value ?? string.Empty).PadRight(widths[column]))) + " |");
}
