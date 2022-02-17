


var DELEGATE_FUNCTION_DELETE_VISIT = null;
class VisitLiteFactory {
    
    buildVisitLites(divBlock, visitLiteViewModels, delegateFunction = null) {
        DELEGATE_FUNCTION_DELETE_VISIT = delegateFunction;
        divBlock.innerHTML = "";
        for (var i = 0; i < visitLiteViewModels.length; i++) {
            divBlock.appendChild(this._buildVisitLite(visitLiteViewModels[i]));
        }
    }

    _buildVisitLite(visitLiteViewModel) {
        var divBlock = document.createElement("div");
        divBlock.className = "blockWithBtnAbonementOrVisit";

        var btnCansel = document.createElement("button");
        btnCansel.onclick = function () {
            ID_OF_VISIT = visitLiteViewModel["id_of_visit"];
            $("#modal_for_buy_visit").modal("hide");
            $("#modalDeleteVisit").modal();
        }
        var imgKrestik = document.createElement("img");
        imgKrestik.src = "/assets/red_krestik.jpg";
        btnCansel.appendChild(imgKrestik);

        var spanDesc = document.createElement("span");
        spanDesc.innerHTML = ' ' + ConvertDateComponent.getDateOrNull(visitLiteViewModel["date_of_buy"]) + " - " + visitLiteViewModel["name_of_dance_group"] + this._getVisitTimeFromTimeTo(visitLiteViewModel) + " (" + visitLiteViewModel["name_of_abonement"] + ")";

        divBlock.appendChild(btnCansel);
        divBlock.appendChild(spanDesc);

        return divBlock;
    }

    _getVisitTimeFromTimeTo(visitLiteViewModel) {
        return " (" + visitLiteViewModel["time_from"] + " - " + visitLiteViewModel["time_to"] + ")";
    }
}