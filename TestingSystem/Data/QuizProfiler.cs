using AutoMapper;
using System;
using TestingSystem.DTOs;
using TestingSystem.Models;

namespace TestingSystem.Data
{
    public class QuizProfiler : Profile
    {
        public QuizProfiler()
        {
            CreateMap<TriviaQuestionDTO, TriviaQuestion>();
            CreateMap<TriviaOptionDTO, TriviaOption>();

            CreateMap<OptionDTO, TriviaOption>();
            CreateMap<QuestionDTO, TriviaQuestion>();
            CreateMap<QuizDTO, TriviaQuiz>();

            CreateMap<AnswerDTO, Answer>();
            CreateMap<ActiveTriviaDTO, ActiveTrivia>();

            CreateMap<CourseDto, Course>();


            //CreateMap<List<OptionDTO>, List<TriviaOption>>();
        }
    }
}
