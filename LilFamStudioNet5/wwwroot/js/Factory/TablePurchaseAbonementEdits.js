

class TablePurchaseAbonementEdits {
    create(divBlock, purchaseAbonementEditViewModels, delegateFunction = null) {
        divBlock.innerHTML = "";
        var tableDiscountConnections;
        var tableBody;
        if (purchaseAbonementEditViewModels.length <= 0) return;

        tableDiscountConnections = document.createElement("table");
        tableDiscountConnections.className = "table table-bordered table-hover mb30";

        var thead = document.createElement("thead");
        var tr = document.createElement("tr");

        var arrayOfThName = [
            "ID",
            "Дата покупки",
            "Вид абонемента",
            "Наименование",
            "На сколько дней",
            "Дата активации",
            "Действителен до",
            "На сколько занятий",
            "Осталось",
            "Посещений",
            "Наличными",
            "Безнал",
            "Комментарий при покупке",
            "Редактировать"
        ];
        arrayOfThName.forEach(name => {
            var th = document.createElement("th");
            th.innerHTML = name;
            tr.appendChild(th);
        });
        /*
        var tr = document.createElement("tr");
        var thID = document.createElement("th"); thID.innerHTML = "ID";
        var thDateOfBuy = document.createElement("th"); thDateOfBuy.innerHTML = "Дата покупки";
        var thSpecialStatus = document.createElement("th"); thSpecialStatus.innerHTML = "Вид абонемента";
        var thName = document.createElement("th"); thName.innerHTML = "Наименование";
        var thDays = document.createElement("th"); thDays.innerHTML = "На сколько дней";
        var thDateOfActivation = document.createElement("th"); thDateOfActivation.innerHTML = "Дата активации";
        var thDateMustBeUsedTo = document.createElement("th"); thDateMustBeUsedTo.innerHTML = "Действителен до";
        var thVisits = document.createElement("th"); thVisits.innerHTML = "На сколько занятий";
        var thVisiitsLeft = document.createElement("th"); thVisiitsLeft.innerHTML = "Осталось";
        var thVisitsUsed = document.createElement("th"); thVisitsUsed.innerHTML = "Посещений";
        var thPrice = document.createElement("th"); thPrice.innerHTML = "Наличными";
        var thCashless = document.createElement("th"); thCashless.innerHTML = "Безнал";
        var thComment = document.createElement("th"); thComment.innerHTML = "Комментарий при покупке";
        var thEdit = document.createElement("th"); thEdit.innerHTML = "Редактировать";

        tr.appendChild(thID);
        tr.appendChild(thDateOfBuy);
        tr.appendChild(thSpecialStatus);
        tr.appendChild(thName);
        tr.appendChild(thDays);
        tr.appendChild(thDateOfActivation);
        tr.appendChild(thDateMustBeUsedTo);
        tr.appendChild(XXXXXXXXXXXXXXXXXX);
        tr.appendChild(XXXXXXXXXXXXXXXXXX);
        */

        thead.appendChild(tr);
        tableDiscountConnections.appendChild(thead);

        tableBody = document.createElement("tbody");

        for (var i = 0; i < purchaseAbonementEditViewModels.length; i++) {
            tableBody.appendChild(this._buildDiscountConnectionToUser(purchaseAbonementEditViewModels[i]));
        }
        tableDiscountConnections.appendChild(tableBody);
        divBlock.appendChild(tableDiscountConnections);
    }


    _buildDiscountConnectionToUser(purchaseAbonementEditViewModel) {
        var tr = document.createElement("tr");
        var tdID = document.createElement("td"); tdID.innerHTML = purchaseAbonementEditViewModel["id"];
        var tdDateOfBuy = document.createElement("td");
        tdDateOfBuy.innerHTML = (purchaseAbonementEditViewModel["dateOfBuy"] != null ? ConvertDateComponent.getDateOrNull(purchaseAbonementEditViewModel["dateOfBuy"]) : "");

        var tdSpecialStatus = document.createElement("td");
        tdSpecialStatus.innerHTML = (
            purchaseAbonementEditViewModel["specialStatus"] == "usual" ? "Абонемент"
                : purchaseAbonementEditViewModel["specialStatus"] == "raz" ? "Разовое"
                    : purchaseAbonementEditViewModel["specialStatus"] == "unlim" ? "Безлимитное" : ""
        );

        var tdName = document.createElement("td"); tdName.innerHTML = purchaseAbonementEditViewModel["nameOfAbonement"];
        var tdDays = document.createElement("td"); tdDays.innerHTML = purchaseAbonementEditViewModel["days"];

        var tdDateOfActivation = document.createElement("td");
        tdDateOfActivation.innerHTML = (purchaseAbonementEditViewModel["dateOfActivation"] != null ? ConvertDateComponent.getDateOrNull(purchaseAbonementEditViewModel["dateOfActivation"]) : "- не акт -");

        var tdDateMustBeUsedTo = document.createElement("td");
        tdDateMustBeUsedTo.innerHTML = (purchaseAbonementEditViewModel["dateMustBeUsedTo"] != null ? ConvertDateComponent.getDateOrNull(purchaseAbonementEditViewModel["dateMustBeUsedTo"]) : "- не акт -");

        var tdVisits = document.createElement("td"); tdVisits.innerHTML = purchaseAbonementEditViewModel["visits"];
        var tdVisiitsLeft = document.createElement("td"); tdVisiitsLeft.innerHTML = purchaseAbonementEditViewModel["visitsLeft"];
        var tdVisitsUsed = document.createElement("td"); tdVisitsUsed.innerHTML = purchaseAbonementEditViewModel["visitsUsed"];
        var tdPrice = document.createElement("td"); tdPrice.innerHTML = purchaseAbonementEditViewModel["price"];
        var tdCashless = document.createElement("td"); tdCashless.innerHTML = purchaseAbonementEditViewModel["cashless"];
        var tdComment = document.createElement("td"); tdComment.innerHTML = purchaseAbonementEditViewModel["comment"];
        var tdEdit = document.createElement("td"); tdEdit.appendChild(this._buildPurchaseAbonementEditBtnEdit(purchaseAbonementEditViewModel));

        tr.appendChild(tdID);
        tr.appendChild(tdDateOfBuy);
        tr.appendChild(tdSpecialStatus);
        tr.appendChild(tdName);
        tr.appendChild(tdDays);
        tr.appendChild(tdDateOfActivation);
        tr.appendChild(tdDateMustBeUsedTo);
        tr.appendChild(tdVisits);
        tr.appendChild(tdVisiitsLeft);
        tr.appendChild(tdVisitsUsed);
        tr.appendChild(tdPrice);
        tr.appendChild(tdCashless);
        tr.appendChild(tdComment);
        tr.appendChild(tdEdit);

        return tr;
    }



    _buildPurchaseAbonementEditBtnEdit(purchaseAbonementEditViewModel) {
        var btnEdit = document.createElement("button");
        btnEdit.className = "btn btn-sm btn-info";
        btnEdit.innerHTML = "Редактировать";
        btnEdit.onclick = function () {
            _userCardLoadPurchaseAbonementEdit(purchaseAbonementEditViewModel["id"]);
        }
        return btnEdit;
    }
}

