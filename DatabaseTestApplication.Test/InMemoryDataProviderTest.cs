﻿using DatabaseTestApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace DatabaseTestApplication.Test
{
    public class InMemoryDataProviderTest
    {
        [Fact]
        public void Task_Add_Without_Relation()
        {
            //Arrange    
            var factory = new ConnectionFactory();

            //Get the instance of BlogDBContext  
            var context = factory.CreateContextForInMemory();

            var post = new Post() { Title = "Test Title 3", Description = "Test Description 3", CreatedDate = DateTime.Now };

            //Act    
            var data = context.Post.Add(post);
            context.SaveChanges();

            //Assert    
            //Get the post count  
            var postCount = context.Post.Count();
            if (postCount != 0)
            {
                Assert.Equal(1, postCount);
            }

            //Get single post detail  
            var singlePost = context.Post.FirstOrDefault();
            if (singlePost != null)
            {
                Assert.Equal("Test Title 3", singlePost.Title);
            }
        }

        [Fact]
        public void Task_Add_With_Relation()
        {
            //Arrange    
            var factory = new ConnectionFactory();

            //Get the instance of BlogDBContext  
            var context = factory.CreateContextForInMemory();

            var post = new Post() { Title = "Test Title 3", Description = "Test Description 3", CategoryId = 2, CreatedDate = DateTime.Now };

            //Act    
            var data = context.Post.Add(post);
            context.SaveChanges();

            //Assert    
            //Get the post count  
            var postCount = context.Post.Count();
            if (postCount != 0)
            {
                Assert.Equal(1, postCount);
            }

            //Get single post detail  
            var singlePost = context.Post.FirstOrDefault();
            if (singlePost != null)
            {
                Assert.Equal("Test Title 3", singlePost.Title);
            }
        }

        [Fact]
        public void Task_Add_Time_Test()
        {
            //Arrange    
            var factory = new ConnectionFactory();

            //Get the instance of BlogDBContext  
            var context = factory.CreateContextForInMemory();

            //Act   
            for (int i = 1; i <= 1000; i++)
            {
                var post = new Post() { Title = "Test Title " + i, Description = "Test Description " + i, CategoryId = 2, CreatedDate = DateTime.Now };
                context.Post.Add(post);
            }

            context.SaveChanges();


            //Assert    
            //Get the post count  
            var postCount = context.Post.Count();
            if (postCount != 0)
            {
                Assert.Equal(1000, postCount);
            }

            //Get single post detail  
            var singlePost = context.Post.Where(x => x.Id == 1).FirstOrDefault();
            if (singlePost != null)
            {
                Assert.Equal("Test Title 1", singlePost.Title);
            }
        }
    }
}
