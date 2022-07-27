<template>
  <div>{{ recipes }}</div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { recipesService } from '../services/RecipesService'

export default {
  name: 'Home',
  setup() {
    onMounted(async () => {
      await recipesService.getAllRecipes()
    })
    return {
      recipes: computed(() => AppState.recipes)
    }
  }
}
</script>

<style scoped lang="scss">
</style>
