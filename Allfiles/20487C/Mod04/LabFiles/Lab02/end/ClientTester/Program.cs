using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace ClientTester
{
    class Program
    {
        static string BASE_ADDRESS = "http://localhost:1885";

        static void Main(string[] args)
        {
            TestService().Wait();
            Console.WriteLine("Press enter to close the window");
            Console.ReadLine();
        }

        private static async Task TestService()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_ADDRESS);

                // Create a new blog
                Blog newBlog = new Blog
                {
                    Name = "New Blog"
                };

                HttpResponseMessage newBlogResponse =
                    await client.PostAsync<Blog>("api/blogs", newBlog, new JsonMediaTypeFormatter());

                if (newBlogResponse.StatusCode != HttpStatusCode.Created)
                {
                    throw new Exception("Failed on creating blog");
                }
                else
                {
                    Console.WriteLine("Blog created successfully");
                }

                // Verify blog was created
                Uri blogUri = newBlogResponse.Headers.Location;
                var response = await client.GetAsync(blogUri);
                Blog blog = await response.Content.ReadAsAsync<Blog>();

                if (!response.IsSuccessStatusCode && blog == null)
                {
                    throw new Exception("Failed retrieving new blog");
                }
                else
                {
                    Console.WriteLine("New blog was found");
                }

                // Create two new blog post
                Post newPost1 = new Post
                {
                    BlogId = newBlog.Id,
                    PublishDate = DateTime.Now,
                    Title = "New Post 1",
                    Content = "This is my first post"
                };

                Post newPost2 = new Post
                {
                    BlogId = newBlog.Id,
                    PublishDate = DateTime.Now,
                    Title = "New Post 2",
                    Content = "This is my second post"
                };

                // Build the new post url based on the blog's URI                
                string postsUri = blogUri.ToString() + "/posts";
                HttpResponseMessage newPostResponse =
                   await client.PostAsync<Post>(postsUri, newPost1, new JsonMediaTypeFormatter());

                if (newPostResponse.StatusCode != HttpStatusCode.Created)
                {
                    throw new Exception("Failed on creating post");
                }
                else
                {
                    Console.WriteLine("Blog post created successfully");
                }

                newPostResponse =
                    await client.PostAsync<Post>(postsUri, newPost2, new JsonMediaTypeFormatter());

                // Verify blog posts were created           
                response = await client.GetAsync(postsUri);               

                IEnumerable<Post> posts = await response.Content.ReadAsAsync<IEnumerable<Post>>();

                if (posts.Count() != 2)
                {
                    throw new Exception("Added two new post but some or all were not found");
                }
                else
                {
                    Console.WriteLine("New blog posts were found");
                }

                // Delete the newly created blog post
                string postUri = postsUri.ToString() + "/" + posts.First().Id.ToString();
                response = await client.DeleteAsync(postUri);
                response.EnsureSuccessStatusCode();
                Console.WriteLine("Blog post was deleted successfully");

                response = await client.GetAsync(postsUri);
                response.EnsureSuccessStatusCode();
                posts = await response.Content.ReadAsAsync<IEnumerable<Post>>();

                if (posts.Count() != 1)
                {
                    throw new Exception("Deleted a post, but it was not deleted");
                }
            }
        }
    }
}
