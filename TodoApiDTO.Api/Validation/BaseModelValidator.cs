using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoApiDTO.Api.Validation
{
    public abstract class BaseModelValidator<TModel> : IModelValidator<TModel>
    {
        private readonly List<Func<TModel, Task<string>>> _rules;

        protected BaseModelValidator()
        {
            _rules = new List<Func<TModel, Task<string>>>();
        }

        protected void AddRule(Func<TModel, Task<string>> rule)
        {
            _rules.Add(rule);
        }

        #region IModelValidator<TModel> Members

        public async Task<ValidationResult> CheckAsync(TModel model)
        {
            foreach (var rule in _rules)
            {
                var message = await rule(model);

                if (message != null)
                {
                    return ValidationResultFactory.GetFailedResult(message);
                }
            }

            return ValidationResultFactory.GetSuccessResult();
        }

        #endregion
    }
}