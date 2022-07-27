import { AppState } from "../AppState";
import { api } from "./AxiosService"

class RecipesService {

    async getAllRecipes() {
        const res = await api.get('api/recipes')
        console.log("recipes", res.data);
        AppState.recipes = res.data
    }

}

export const recipesService = new RecipesService()