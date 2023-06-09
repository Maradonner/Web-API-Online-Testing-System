using AutoMapper;
using TestingSystem.DTOs;
using TestingSystem.Models;

namespace TestingSystem.Data;

public class QuizProfiler : Profile
{
    public QuizProfiler()
    {
        CreateMap<QuizDto, TriviaQuiz>();
        CreateMap<AnswerDto, Answer>();
        CreateMap<CourseDto, Course>();

        CreateMap<TriviaQuiz, QuizForDisplayDto>().ReverseMap();
        CreateMap<TriviaQuiz, QuizForCreationDto>().ReverseMap();
        CreateMap<TriviaQuiz, QuizForUpdateDto>().ReverseMap();

        CreateMap<TriviaQuestion, QuestionForDisplayDto>().ReverseMap();
        CreateMap<TriviaQuestion, QuestionForCreationDto>().ReverseMap();
        CreateMap<TriviaQuestion, QuestionForUpdateDto>().ReverseMap();

        CreateMap<TriviaOption, OptionForDisplayDto>().ReverseMap();
        CreateMap<TriviaOption, OptionForCreationDto>().ReverseMap();
        CreateMap<TriviaOption, OptionForUpdateDto>().ReverseMap();

        CreateMap<UserForDisplayDto, User>().ReverseMap();
    }
}