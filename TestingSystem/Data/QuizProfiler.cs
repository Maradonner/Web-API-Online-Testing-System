using AutoMapper;
using TestingSystem.DTOs;
using TestingSystem.Models;

namespace TestingSystem.Data;

public class QuizProfiler : Profile
{
    public QuizProfiler()
    {
        CreateMap<OptionDto, TriviaOption>();
        CreateMap<QuestionDto, TriviaQuestion>();
        CreateMap<QuizDto, TriviaQuiz>();

        CreateMap<AnswerDto, Answer>();

        CreateMap<CourseDto, Course>();
    }
}