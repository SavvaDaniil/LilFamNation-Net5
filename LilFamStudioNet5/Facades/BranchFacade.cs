using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.Branch;
using LilFamStudioNet5.Entities;
using LilFamStudioNet5.Services;
using LilFamStudioNet5.ViewModels;
using LilFamStudioNet5.ViewModels.Branch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Facades
{
    public class BranchFacade
    {
        public ApplicationDbContext _dbc;
        public BranchFacade(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }


        public async Task<List<BranchLiteViewModel>> listAllLite()
        {
            BranchService branchService = new BranchService(_dbc);
            List<Branch> branches = await branchService.listAll();
            List<BranchLiteViewModel> branchLiteViewModels = new List<BranchLiteViewModel>();
            int i = 0;
            foreach (Branch branch in branches)
            {
                i++;
                branchLiteViewModels.Add(new BranchLiteViewModel(i, branch.id, branch.name, branch.coordinates));
            }

            return branchLiteViewModels;
        }


        public async Task<BranchEditViewModel> getEdit(int id)
        {
            BranchService branchService = new BranchService(_dbc);
            Branch branch = await branchService.findById(id);
            if (branch == null) return null;

            BranchEditViewModel branchEditViewModel = new BranchEditViewModel(
                branch.id,
                branch.name,
                branch.description,
                branch.coordinates
            );

            return branchEditViewModel;
        }

        public async Task<BranchLiteViewModel> getLiteBydId(int id)
        {
            BranchService branchService = new BranchService(_dbc);
            Branch branch = await branchService.findById(id);
            if (branch == null) return null;
            return new BranchLiteViewModel(branch.id, branch.name);
        }

        public async Task<Branch> add(BranchNewDTO branchNewDTO)
        {
            BranchService branchService = new BranchService(_dbc);
            if (branchNewDTO.name == null) return null;
            Branch branch = await branchService.add(branchNewDTO.name);
            return branch;
        }

        public async Task<bool> delete(int id)
        {
            BranchService branchService = new BranchService(_dbc);
            Branch branch = await branchService.findById(id);
            if (branch == null) return false;

            return await branchService.delete(branch);
        }

        public async Task<JsonAnswerStatus> update(BranchEditByColumnDTO branchEditByColumnDTO)
        {
            BranchService branchService = new BranchService(_dbc);
            Branch branch = await branchService.findById(branchEditByColumnDTO.id_of_branch);
            if (branch == null) return new JsonAnswerStatus("error", "not_found");

            await branchService.updateByColumn(branch, branchEditByColumnDTO.name, branchEditByColumnDTO.value);

            return new JsonAnswerStatus("success", null);
        }



    }
}
