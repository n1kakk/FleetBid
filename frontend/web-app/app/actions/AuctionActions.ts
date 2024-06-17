﻿'use server'

import { fetchWrapper } from '@/lib/fetchWrapper';
import {PagedResult, Auction } from '../types';
import { FieldValues } from 'react-hook-form';
import { revalidatePath } from 'next/cache';

export async function GetData(query: string): Promise<PagedResult<Auction>>{
    return await fetchWrapper.get(`search${query}`)
}

export async function createAuction(data: FieldValues){
    return await fetchWrapper.post('auctions', data);
}

export async function getDetailedViewData(id: string): Promise<Auction>{
    return await fetchWrapper.get(`auctions/${id}`);
}

export async function updateAuction(data: FieldValues, id: string){
    const res = await fetchWrapper.put(`auctions/${id}`, data);
    revalidatePath(`/auctions/${id}`);
    return res;
}

export async function deleteAuction(id: string){
    return await fetchWrapper.del(`auctions/${id}`);
}