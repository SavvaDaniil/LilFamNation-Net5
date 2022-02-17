

var DELEGATE_FUNCTION_DISCOUNT_CONNECTION = null;
class DiscountConnectionsFactory {
    
    create(divBlock, discountWithConnectionToUserLiteViewModels, delegateFunction = null) {
        divBlock.innerHTML = "";
        var tableDiscountConnections;
        var tableBody;
        if (discountWithConnectionToUserLiteViewModels.length <= 0) return;

        tableDiscountConnections = document.createElement("table");
        tableDiscountConnections.className = "table table-striped table-bordered mb30";
        var thead = document.createElement("thead");
        var tr = document.createElement("tr");
        var thID = document.createElement("th"); thID.innerHTML = "ID";
        var thName = document.createElement("th"); thName.innerHTML = "Наименование";
        var thStatus = document.createElement("th"); thStatus.innerHTML = "Статус";
        tr.appendChild(thID);
        tr.appendChild(thName);
        tr.appendChild(thStatus);
        thead.appendChild(tr);
        tableDiscountConnections.appendChild(thead);

        tableBody = document.createElement("tbody");

        for (var i = 0; i < discountWithConnectionToUserLiteViewModels.length; i++) {
            tableBody.appendChild(this._buildDiscountConnectionToUser(discountWithConnectionToUserLiteViewModels[i]));
        }
        tableDiscountConnections.appendChild(tableBody);
        divBlock.appendChild(tableDiscountConnections);
    }
    _buildDiscountConnectionToUser(discountWithConnectionToUserLiteViewModel) {
        var tr = document.createElement("tr");
        var tdID = document.createElement("td"); tdID.innerHTML = discountWithConnectionToUserLiteViewModel["id"];
        var tdName = document.createElement("td"); tdName.innerHTML = discountWithConnectionToUserLiteViewModel["name"];
        var tdStatus = document.createElement("td"); tdStatus.appendChild(this._buildDiscountConnectionToUserStatus(discountWithConnectionToUserLiteViewModel));

        tr.appendChild(tdID);
        tr.appendChild(tdName);
        tr.appendChild(tdStatus);
        return tr;
    }
    _buildDiscountConnectionToUserStatus(discountWithConnectionToUserLiteViewModel) {
        var select = document.createElement("select");
        select.className = "form-control";
        select.onchange = function () {
            _userCardUpdateDiscountConnectionToUser(discountWithConnectionToUserLiteViewModel["id"], this.value);
        }
        var option0 = document.createElement("option"); option0.value = 0; option0.innerHTML = "Нет";
        option0.selected = (discountWithConnectionToUserLiteViewModel["status"] == 0 ? "selected" : "");
        var option1 = document.createElement("option"); option1.value = 1; option1.innerHTML = "Да";
        option1.selected = (discountWithConnectionToUserLiteViewModel["status"] == 1 ? "selected" : "");
        select.appendChild(option0);
        select.appendChild(option1);
        return select;
    }

}
