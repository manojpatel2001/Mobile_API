using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mobile_Core.ViewModel.TaxDocument;
using Mobile_Infrastructure.Interface;
using Mobile_Utility;

namespace Mobile_API.Controllers.TaxDocument
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaxDocumentAPIController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaxDocumentAPIController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Fetch all tax documents for a user.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>APIResponse with tax documents</returns>
        [HttpGet("GetTaxDocuments/{userId}")]
        public async Task<APIResponse> GetTaxDocuments(int userId)
        {
            try
            {
                var data = await _unitOfWork.TaxDocumentRepository.GetTaxDocuments(userId);
                return data;
            }
            catch
            {
                return new APIResponse
                {
                    Status = false,
                    ResponseMessage = "Unable to retrieve tax documents. Please try again later."
                };
            }
        }

        /// <summary>
        /// Insert a new tax document.
        /// </summary>
        /// <param name="taxDocumentVM">Tax document view model</param>
        /// <returns>APIResponse with operation status</returns>
        [HttpPost("InsertTaxDocument")]
        public async Task<APIResponse> InsertTaxDocument(TaxDocuments taxDocumentVM)
        {
            try
            {
                if (taxDocumentVM.UserId == 0)
                {
                    return new APIResponse
                    {
                        Status = false,
                        ResponseMessage = "Please provide a valid user ID!"
                    };
                }

                if (taxDocumentVM.FinancialYear==0|| taxDocumentVM.FinancialYear==null)
                {
                    return new APIResponse
                    {
                        Status = false,
                        ResponseMessage = "Please provide a valid financial year!"
                    };
                }

                var data = await _unitOfWork.TaxDocumentRepository.InsertTaxDocument(taxDocumentVM);
                return data;
            }
            catch
            {
                return new APIResponse
                {
                    Status = false,
                    ResponseMessage = "Unable to insert tax document. Please try again later."
                };
            }
        }

        /// <summary>
        /// Insert a new tax declaration.
        /// </summary>
        /// <param name="taxDeclarationVM">Tax declaration view model</param>
        /// <returns>APIResponse with operation status</returns>
        [HttpPost("InsertDeclaration")]
        public async Task<APIResponse> InsertDeclaration(TaxDeclaration taxDeclaration)
        {
            try
            {
                if (taxDeclaration.UserId == 0)
                {
                    return new APIResponse
                    {
                        Status = false,
                        ResponseMessage = "Please provide a valid user ID!"
                    };
                }

                if (taxDeclaration.FinancialYear==0|| taxDeclaration.FinancialYear == null)
                {
                    return new APIResponse
                    {
                        Status = false,
                        ResponseMessage = "Please provide a valid financial year!"
                    };
                }

                var data = await _unitOfWork.TaxDocumentRepository.InsertDeclaration(taxDeclaration);
                return data;
            }
            catch
            {
                return new APIResponse
                {
                    Status = false,
                    ResponseMessage = "Unable to insert declaration. Please try again later."
                };
            }
        }

        /// <summary>
        /// Update an existing tax declaration.
        /// </summary>
        /// <param name="taxDeclarationVM">Tax declaration view model</param>
        /// <returns>APIResponse with operation status</returns>
        [HttpPost("UpdateDeclaration")]
        public async Task<APIResponse> UpdateDeclaration(TaxDeclaration taxDeclarationVM)
        {
            try
            {
                if (taxDeclarationVM.UserId == 0)
                {
                    return new APIResponse
                    {
                        Status = false,
                        ResponseMessage = "Please provide a valid user ID!"
                    };
                }

                if (string.IsNullOrEmpty(taxDeclarationVM.DeclarationType))
                {
                    return new APIResponse
                    {
                        Status = false,
                        ResponseMessage = "Please provide a valid declaration type!"
                    };
                }

                var data = await _unitOfWork.TaxDocumentRepository.UpdateDeclaration(taxDeclarationVM);
                return data;
            }
            catch
            {
                return new APIResponse
                {
                    Status = false,
                    ResponseMessage = "Unable to update declaration. Please try again later."
                };
            }
        }

        /// <summary>
        /// Fetch tax summary for a user and financial year.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="financialYear">Financial year</param>
        /// <returns>APIResponse with tax summary</returns>
        [HttpPost("GetTaxSummary")]
        public async Task<APIResponse> GetTaxSummary(TaxSummaryVM model)
        {
            try
            {
                if (model.UserId == 0)
                {
                    return new APIResponse
                    {
                        Status = false,
                        ResponseMessage = "Please provide a valid user ID!"
                    };
                }

                if (model.FinancialYear == 0 || model.FinancialYear == null)
                {
                    return new APIResponse
                    {
                        Status = false,
                        ResponseMessage = "Please provide a valid financial year!"
                    };
                }

                var data = await _unitOfWork.TaxDocumentRepository.GetTaxSummary(model);
                return data;
            }
            catch
            {
                return new APIResponse
                {
                    Status = false,
                    ResponseMessage = "Unable to retrieve tax summary. Please try again later."
                };
            }
        }

    }
}
