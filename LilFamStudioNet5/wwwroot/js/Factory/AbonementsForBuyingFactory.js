

class AbonementsForBuyingFactory {

    create(divBlock, abonementsBySpecialStatusViewModel, delegateAfterBuyAbonement = null, id_of_dance_group = 0, id_of_dance_group_day_of_week = 0, date_of_lesson = null) {
        delegateFunctionAfterBuyAbonement = delegateAfterBuyAbonement;

        divBlock.innerHTML = "";
        divBlock.appendChild(this._buildHeaderAbonementsForBuying("Разовые занятия"));
        for (var i = 0; i < abonementsBySpecialStatusViewModel["abonementLiteViewModelsRaz"].length; i++) {
            divBlock.appendChild(this._buildAbonementForBuying(abonementsBySpecialStatusViewModel["abonementLiteViewModelsRaz"][i],
                id_of_dance_group, id_of_dance_group_day_of_week, date_of_lesson));
        }
        divBlock.appendChild(this._buildHeaderAbonementsForBuying("Обычные абонементы"));
        for (var i = 0; i < abonementsBySpecialStatusViewModel["abonementLiteViewModelsUsual"].length; i++) {
            divBlock.appendChild(this._buildAbonementForBuying(abonementsBySpecialStatusViewModel["abonementLiteViewModelsUsual"][i],
                id_of_dance_group, id_of_dance_group_day_of_week, date_of_lesson));
        }
        divBlock.appendChild(this._buildHeaderAbonementsForBuying("Пробный разовые занятия"));
        for (var i = 0; i < abonementsBySpecialStatusViewModel["abonementLiteViewModelsRazTrial"].length; i++) {
            divBlock.appendChild(this._buildAbonementForBuying(abonementsBySpecialStatusViewModel["abonementLiteViewModelsRazTrial"][i],
                id_of_dance_group, id_of_dance_group_day_of_week, date_of_lesson));
        }
        divBlock.appendChild(this._buildHeaderAbonementsForBuying("Безлимитные занятия"));
        for (var i = 0; i < abonementsBySpecialStatusViewModel["abonementLiteViewModelsUnlim"].length; i++) {
            divBlock.appendChild(this._buildAbonementForBuying(abonementsBySpecialStatusViewModel["abonementLiteViewModelsUnlim"][i],
                id_of_dance_group, id_of_dance_group_day_of_week, date_of_lesson));
        }

    }

    _buildHeaderAbonementsForBuying(name) {
        var p = document.createElement("p");
        p.className = "userCardAbonementForBuyingHeader";
        p.innerHTML = name;
        return p;
    }

    _buildAbonementForBuying(abonementLiteViewModel, id_of_dance_group = 0, id_of_dance_group_day_of_week = 0, date_of_lesson = null) {
        var p = document.createElement("p");
        var btnBuy = document.createElement("button");
        btnBuy.type = "button";
        btnBuy.className = "btn btn-sm btn-default";
        btnBuy.innerHTML = "Оформить покупку";
        btnBuy.onclick = function () {
            open_modal_new_abonement(ID_OF_USER, abonementLiteViewModel["id"], date_of_lesson, id_of_dance_group, id_of_dance_group_day_of_week);
        }
        p.appendChild(btnBuy);
        var spanDesc = document.createElement("span");
        spanDesc.innerHTML = " - " + abonementLiteViewModel["name"] + " (" + abonementLiteViewModel["price"] + " рублей)";
        p.appendChild(spanDesc);
        return p;
    }
}