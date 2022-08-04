<template>
  <div class="container">
    <div class="row">
      <div class="col-12">
        <div class="d-flex my-5">

          <img :src="account.picture" />
          <div class="display-4 fw-bold p-3">
            {{ account.name }}
          </div>

        </div>
        <div class="ms-5 ps-5 fs-2 fst-italic">
          <div>Vaults: {{ vaults.length }}</div>
          <div>Keeps: {{ keeps.length }}</div>
        </div>
      </div>
      <!-- Vaults -->
      <div class="col-12 my-5">
        <div class="d-flex justify-content-between">
          <div class="display-6 fw-bold text-decoration-underline">Vaults</div>
          <div>
            <button class="btn btn-outline-secondary">
              New Vault
            </button>
          </div>
        </div>
        <div>
          <div class="row">
            <!-- Template - Componentize -->
            <Vault v-for="v in vaults" :vault="v" class="my-2 my-md-1" />
          </div>
        </div>

        <div class="row my-5">
          <div class="col-12">

            <div class="d-flex justify-content-between">
              <div class="display-6 fw-bold text-decoration-underline">Keeps</div>
              <div>
                <button class="btn btn-outline-secondary">
                  New Keep
                </button>
              </div>
            </div>
            <div class="masonry-container ">
              <Keep v-for="k in keeps" :keep="k" :vaults="vaults" :vKeeps="vKeeps" class='masonry-item' />
            </div>
          </div>
        </div>

      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, watchEffect } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from "../services/KeepsService"
import Pop from "../utils/Pop"
import { logger } from "../utils/Logger"
export default {
  name: 'Account',
  setup() {

    return {
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.MyVaults),
      vKeeps: computed(() => AppState.MyVaultKeeps),
      keeps: computed(() => AppState.MyKeeps)
    }
  }
}
</script>

<style scoped>
/* img {
  max-width: 100px;
} */


.masonry-container {
  columns: 6 200px;
  column-gap: 1rem;



}

.masonry-item {
  display: inline-block;
  width: 100%;
  text-align: center;



}
</style>
