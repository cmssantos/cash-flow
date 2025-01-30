using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;

namespace CashFlow.CommonTestUtilities.Requests;

public class RequestRegisterExpenseJsonBuilder
{
    public static RequestRegisterExpenseJson Build()
    {
        return new Faker<RequestRegisterExpenseJson>()
            .RuleFor(x => x.Title, faker => faker.Commerce.Product())
            .RuleFor(x => x.Description, faker => faker.Lorem.Sentence())
            .RuleFor(x => x.Amount, faker => faker.Finance.Amount())
            .RuleFor(x => x.Date, faker => DateOnly.FromDateTime(faker.Date.Past()))
            .RuleFor(x => x.PaymentType, faker => faker.PickRandom<PaymentType>());
    }
}
