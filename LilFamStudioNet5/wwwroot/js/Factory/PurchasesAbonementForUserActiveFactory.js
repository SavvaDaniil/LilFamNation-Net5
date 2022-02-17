
class PurchasesAbonementForUserActiveFactory {

    buildFromList(divBlock, purchaseAbonementLiteViewModels, id_of_dance_group, date_of_day, id_of_dance_group_day_of_week, delegateFunction = null) {
        divBlock.innerHTML = "";
        for (var i = 0; i < purchaseAbonementLiteViewModels.length; i++) {
            divBlock.appendChild(this._buildPurchaseAbonementLiteViewModel(purchaseAbonementLiteViewModels[i], id_of_dance_group, date_of_day, id_of_dance_group_day_of_week, delegateFunction));
        }
    }

    _buildPurchaseAbonementLiteViewModel(purchaseAbonementLiteViewModel, id_of_dance_group, date_of_day, id_of_dance_group_day_of_week, delegateFunction = null) {
        var divBlock = document.createElement("div");
        divBlock.className = "blockWithBtnAbonementOrVisit";

        var btnBlock = document.createElement("button");
        btnBlock.className = "btn btn-default";
        btnBlock.innerHTML = purchaseAbonementLiteViewModel["name"];
        btnBlock.onclick = function () {
            addVisit(id_of_dance_group, id_of_dance_group_day_of_week, purchaseAbonementLiteViewModel["id_of_purchase_abonement"], date_of_day, delegateFunction);
        }
        divBlock.appendChild(btnBlock);

        var spanDesc = document.createElement("span");
        if (purchaseAbonementLiteViewModel["dateOfActivation"] == null) {
            spanDesc.innerHTML = ' - куп. ' + ConvertDateComponent.getDateOrNull(purchaseAbonementLiteViewModel["dateOfBuy"]);
            var spanNotActived = document.createElement("span");
            spanNotActived.className = "success";
            spanNotActived.innerHTML = " не акт";
            spanDesc.appendChild(spanNotActived);
            var aReturn = document.createElement("a");
            aReturn.href = "#";
            aReturn.innerHTML = " ВОЗВРАТ";
            aReturn.onclick = function () {
                ID_OF_PURCHASE_ABONEMENT = purchaseAbonementLiteViewModel["id_of_purchase_abonement"];
                $("#modal_for_buy_visit").modal("hide");
                $("#modalDeletePurchaseAbonement").modal();
            }
            spanDesc.appendChild(aReturn);
        } else {
            if (purchaseAbonementLiteViewModel["specialStatus"] == "inlim") {
                spanDesc.innerHTML = ' - безлимитно';
            } else {
                spanDesc.innerHTML = ' - Осталось ' + purchaseAbonementLiteViewModel["visitsLeft"] + ' из ' + purchaseAbonementLiteViewModel["visits"];
            }
            spanDesc.innerHTML += ' | куп. ' + ConvertDateComponent.getDateOrNull(purchaseAbonementLiteViewModel["dateOfBuy"])
                + ', акт. ' + ConvertDateComponent.getDateOrNull(purchaseAbonementLiteViewModel["dateOfActivation"])
                + ' дейст. до ' + ConvertDateComponent.getDateOrNull(purchaseAbonementLiteViewModel["dateOfMustBeUsedTo"]);
        }
        divBlock.appendChild(spanDesc);


        return divBlock;
    }
}