using Person.Models;
using Person.Date;
using Microsoft.EntityFrameworkCore;

namespace Person.Routes {
    public static class PersonRoute {
        public static void PersonRoutes(this WebApplication app) {

            var route = app.MapGroup("person");

            route.MapPost("",
                async (PersonRequest req, PersonContext context) => {
                    var person = new PersonModel(req.name);
                    await context.AddAsync(person);
                    await context.SaveChangesAsync();
                }
            );

            route.MapGet("", async (PersonContext ctx) => {
                var people = await ctx.People.ToListAsync();
                if (people.Count == 0) {
                    return Results.NoContent();
                }
                return Results.Ok(people);
            });

            route.MapPatch("{id:guid}",
                async (Guid id, PersonRequest req, PersonContext ctx) => {
                    var person = await ctx.People.FirstOrDefaultAsync(x => x.Id == id);

                    if (person == null) 
                        return Results.NotFound();
                    
                    person.ChangeName(req.name);

                    await ctx.SaveChangesAsync();

                    return Results.Ok(person);

                });

            route.MapDelete("{id:guid}",
                    async (Guid id, PersonContext ctx) => {
                        var person = await ctx.People.FirstOrDefaultAsync(x => x.Id == id);

                        if (person == null)
                            return Results.NotFound();

                        ctx.People.Remove(person);

                        await ctx.SaveChangesAsync();
                        return Results.NoContent();
                    }

                );
        }
    }
}
