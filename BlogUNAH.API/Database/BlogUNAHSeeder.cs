﻿using BlogUNAH.API.Constants;
using BlogUNAH.API.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BlogUNAH.API.Database
{
    public class BlogUNAHSeeder
    {
        public static async Task LoadDataAsync(
            BlogUNAHContext context,
            ILoggerFactory loggerFactory,
            UserManager<UserEntity> userManager,
            RoleManager<IdentityRole> roleManager
            ) 
        {
            try
            {
                await LoadRolesAndUsersAsync(userManager, roleManager, loggerFactory);
                await LoadCategoriesAsync(loggerFactory, context);
                await LoadPostsAsync(loggerFactory, context);
                await LoadTagsAsync(loggerFactory, context);
                await LoadPostsTagsAsync(loggerFactory, context);
            }
            catch (Exception e) 
            {
                var logger = loggerFactory.CreateLogger<BlogUNAHSeeder>();
                logger.LogError(e, "Error inicializando la data del API");
            }
        }

        public static async Task LoadRolesAndUsersAsync(
            UserManager<UserEntity> userManager,
            RoleManager<IdentityRole> roleManager,
            ILoggerFactory loggerFactory
            )
        {
            try
            {
                if (!await roleManager.Roles.AnyAsync())
                {
                    await roleManager.CreateAsync(new IdentityRole(RolesConstant.ADMIN));
                    await roleManager.CreateAsync(new IdentityRole(RolesConstant.AUTHOR));
                    await roleManager.CreateAsync(new IdentityRole(RolesConstant.USER));
                }

                //si no hay usuarios que cree lo que esta dentro del condicional
                if (!await userManager.Users.AnyAsync())
                {
                    //aqui podria colocarse en un .json como los demas
                    var userAdmin = new UserEntity
                    {
                        FirstName = "Administrador",
                        LastName = "Blog",
                        Email = "admin@blogunah.edu",
                        UserName = "admin@blogunah.edu",
                    };

                    var userAuthor = new UserEntity
                    {
                        FirstName = "Autor",
                        LastName = "Blog",
                        Email = "author@blogunah.edu",
                        UserName = "author@blogunah.edu",
                    };   

                    var normalUser = new UserEntity
                    {
                        FirstName = "User",
                        LastName = "Blog",
                        Email = "user@blogunah.edu",
                        UserName = "user@blogunah.edu",
                    };

                    //la contraseña es "Temporal"
                    await userManager.CreateAsync(userAdmin, "Temporal01*");
                    await userManager.CreateAsync(userAuthor, "Temporal01*");
                    await userManager.CreateAsync(normalUser, "Temporal01*");

                    await userManager.AddToRoleAsync(userAdmin, RolesConstant.ADMIN);
                    await userManager.AddToRoleAsync(userAuthor, RolesConstant.AUTHOR);
                    await userManager.AddToRoleAsync(normalUser, RolesConstant.USER);
                }
            }
            catch(Exception e)
            {
                var logger = loggerFactory.CreateLogger<BlogUNAHSeeder>();
                logger.LogError(e.Message);
            }
        }

        public static async Task LoadCategoriesAsync(ILoggerFactory loggerFactory, BlogUNAHContext context) 
        {
            try
            {
                var jsonFilePath = "SeedData/categories.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var categories = JsonConvert.DeserializeObject<List<CategoryEntity>>(jsonContent);

                if (!await context.Categories.AnyAsync()) 
                {
                    
                    var user = await context.Users.FirstOrDefaultAsync();

                    for (int i = 0; i < categories.Count; i++) 
                    {
                        categories[i].CreatedBy = user.Id;
                        categories[i].CreatedDate = DateTime.Now;
                        categories[i].UpdatedBy = user.Id;
                        categories[i].UpdatedDate = DateTime.Now;
                    }
                    
                    context.AddRange(categories);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e) 
            {
                var logger = loggerFactory.CreateLogger<BlogUNAHSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed de categorias");
            }
        }

        public static async Task LoadPostsAsync(ILoggerFactory loggerFactory, BlogUNAHContext context)
        {
            try
            {
                var jsonFilePath = "SeedData/posts.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var posts = JsonConvert.DeserializeObject<List<PostEntity>>(jsonContent);

                if (!await context.Posts.AnyAsync())
                {
                    var user = await context.Users.FirstOrDefaultAsync();
                    for (int i = 0; i < posts.Count; i++)
                    {
                        posts[i].AuthorId = user.Id;
                        posts[i].CreatedBy = user.Id;
                        posts[i].CreatedDate = DateTime.Now;
                        posts[i].UpdatedBy = user.Id;
                        posts[i].UpdatedDate = DateTime.Now;
                    }

                    context.AddRange(posts);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<BlogUNAHSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed de Posts");
            }
        }

        public static async Task LoadTagsAsync(ILoggerFactory loggerFactory, BlogUNAHContext context)
        {
            try
            {
                var jsonFilePath = "SeedData/tags.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var tags = JsonConvert.DeserializeObject<List<TagEntity>>(jsonContent);

                if (!await context.Tags.AnyAsync())
                {

                    var user = await context.Users.FirstOrDefaultAsync();
                    for (int i = 0; i < tags.Count; i++)
                    {
                        tags[i].CreatedBy = user.Id;
                        tags[i].CreatedDate = DateTime.Now;
                        tags[i].UpdatedBy = user.Id;
                        tags[i].UpdatedDate = DateTime.Now;
                    }

                    context.AddRange(tags);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<BlogUNAHSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed de Tags");
            }
        }

        public static async Task LoadPostsTagsAsync(ILoggerFactory loggerFactory, BlogUNAHContext context)
        {
            try
            {
                var jsonFilePath = "SeedData/posts_tags.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var postTags = JsonConvert.DeserializeObject<List<PostTagEntity>>(jsonContent);

                if (!await context.PostsTags.AnyAsync())
                {
                    var user = await context.Users.FirstOrDefaultAsync();

                    for (int i = 0; i < postTags.Count; i++)
                    {
                        postTags[i].CreatedBy = user.Id;
                        postTags[i].CreatedDate = DateTime.Now;
                        postTags[i].UpdatedBy = user.Id;
                        postTags[i].UpdatedDate = DateTime.Now;
                    }

                    context.AddRange(postTags);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<BlogUNAHSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed de PostsTags");
            }
        }
    }
}
