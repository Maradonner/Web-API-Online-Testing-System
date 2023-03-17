//using TestingSystem.Models;

//namespace TestingSystem.Data
//{
//    //public class Seed
//    //{
//    //    public static void SeedData(IApplicationBuilder applicationBuilder)
//    //    {
//    //        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
//    //        {
//    //            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

//    //            context.Database.EnsureCreated();

//    //            if (!context.TriviaQuestions.Any())
//    //            {
//    //                context.TriviaQuestions.AddRange(new List<TriviaQuestion>()
//    //                {
//    //                    new TriviaQuestion()
//    //                    {
//    //                        Title = "In which Italian city can you find the Colosseum?",
//    //                        Options =
//    //                        {
//    //                            new TriviaOption()
//    //                            {
//    //                                Title = "Venice",
//    //                                IsCorrect = false
//    //                            },
//    //                            new TriviaOption()
//    //                            {
//    //                                Title = "Rome",
//    //                                IsCorrect = true
//    //                            },
//    //                            new TriviaOption()
//    //                            {
//    //                                Title = "Naples",
//    //                                IsCorrect = false
//    //                            },
//    //                            new TriviaOption()
//    //                            {
//    //                                Title = "Milan",
//    //                                IsCorrect = false
//    //                            }
//    //                        }
//    //                    },
//    //                    new TriviaQuestion()
//    //                    {
//    //                        Title = "When did Lenin die?",
//    //                        Options =
//    //                        {
//    //                            new TriviaOption()
//    //                            {
//    //                                Title = "21 January 1924",
//    //                                IsCorrect = false
//    //                            },
//    //                            new TriviaOption()
//    //                            {
//    //                                Title = "21 January 1934",
//    //                                IsCorrect = true
//    //                            },
//    //                            new TriviaOption()
//    //                            {
//    //                                Title = "21 January 1944",
//    //                                IsCorrect = false
//    //                            },
//    //                            new TriviaOption()
//    //                            {
//    //                                Title = "21 January 1954",
//    //                                IsCorrect = false
//    //                            },
//    //                        },
//    //                    },
//    //                    new TriviaQuestion()
//    //                    {
//    //                        Title = "When did Hitler invade Poland?",
//    //                        Options =
//    //                        {
//    //                            new TriviaOption()
//    //                            {
//    //                                Title = "1st September 1939",
//    //                                IsCorrect = true
//    //                            },
//    //                            new TriviaOption()
//    //                            {
//    //                                Title = "11th November 1939",
//    //                                IsCorrect = false
//    //                            },
//    //                            new TriviaOption()
//    //                            {
//    //                                Title = "8th May 1940",
//    //                                IsCorrect = false
//    //                            },
//    //                            new TriviaOption()
//    //                            {
//    //                                Title = "1st December 1940",
//    //                                IsCorrect = false
//    //                            },
//    //                        },
//    //                    }
//    //                });
//    //                context.SaveChanges();
//    //            }
//    //        }
//    //    }
//    //}


//    public class Seed
//    {
//        public static void SeedData(IApplicationBuilder applicationBuilder)
//        {
//            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
//            {
//                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

//                context.Database.EnsureCreated();

//                if (!context.TriviaQuestions.Any())
//                {
//                    context.TriviaQuestions.AddRange(new List<TriviaQuestionDTO>()
//                    {
//                        new TriviaQuestionDTO()
//                        {
//                            Title = "In which Italian city can you find the Colosseum?",
//                            Options = new List<TriviaOption>()
//                            {
//                                new TriviaOption()
//                                {
//                                    Title = "Venice",
//                                    IsCorrect = false
//                                },
//                                new TriviaOption()
//                                {
//                                    Title = "Rome",
//                                    IsCorrect = true
//                                },
//                                new TriviaOption()
//                                {
//                                    Title = "Naples",
//                                    IsCorrect = false
//                                },
//                                new TriviaOption()
//                                {
//                                    Title = "Milan",
//                                    IsCorrect = false
//                                }
//                            }
//                        },
//                        new TriviaQuestionDTO()
//                        {
//                            Title = "When did Lenin die?",
//                            Options = new List<TriviaOption>()
//                            {
//                                new TriviaOption()
//                                {
//                                    Title = "21 January 1924",
//                                    IsCorrect = false
//                                },
//                                new TriviaOption()
//                                {
//                                    Title = "21 January 1934",
//                                    IsCorrect = true
//                                },
//                                new TriviaOption()
//                                {
//                                    Title = "21 January 1944",
//                                    IsCorrect = false
//                                },
//                                new TriviaOption()
//                                {
//                                    Title = "21 January 1954",
//                                    IsCorrect = false
//                                },
//                            },
//                        }

//                    });
//                    context.SaveChanges();
//                }
//            }
//        }
//    }

//}
