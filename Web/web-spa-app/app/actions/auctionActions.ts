'use server'

import { fetchWrapper } from "@/lib/fetchWrapper"
import { Auction, PagedResult } from "@/types"
import { FieldValues } from "react-hook-form"

export async function getData(url: string): Promise<PagedResult<Auction>> {
  return await fetchWrapper.get(`search${url}`)
}

export async function createAuction(data: FieldValues) {
  return await fetchWrapper.post('auctions', data)
}