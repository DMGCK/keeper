import { applyStyles } from "@popperjs/core";
import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class KeepsService {
  async GetKeeps() {
    const res = await api.get("api/keeps")
    logger.log('[GetKeeps]', res.data);
    AppState.keeps = res.data;
  }

  async GetKeepById(id) {
    const res = await api.get(`api/keeps/${id}`);
    logger.log('[GetKeepsById]', res.data)
    AppState.ActiveKeep = res.data;
    return res.data;
  }
  async deleteKeep(id) {
    const res = await api.delete(`api/keeps/${id}`)
    AppState.MyKeeps = AppState.MyKeeps.filter(k => k.id != id)
    logger.log('[DeleteKeep]', res.data);
  }

  async GetActiveUserKeeps(userId) {
    const res = await api.get(`api/profiles/${userId}/keeps`)
    logger.log("[GetActiveUserKeeps]", res.data)

    if (AppState.account.id == userId) {
      AppState.MyKeeps = res.data
      return
    }
    AppState.ActiveUserKeeps = res.data;
  }

  async createKeep(keepData) {
    const res = await api.post(`api/keeps`, keepData);
    console.log('[CreateKeep]', res.data);
    AppState.MyKeeps = [res.data, ...AppState.MyKeeps]
  }

}

export const keepsService = new KeepsService();