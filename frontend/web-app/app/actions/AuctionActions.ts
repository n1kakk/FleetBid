'use server'

import { fetchWrapper } from '@/lib/fetchWrapper';
import {PagedResult, Auction } from '../types';
import { FieldValues } from 'react-hook-form';

export async function GetData(query: string): Promise<PagedResult<Auction>>{
    return await fetchWrapper.get(`search${query}`)
}

export async function createAuction(data: FieldValues){
    return await fetchWrapper.post('auctions', data);
}

export async function getDetailedViewData(id: string): Promise<Auction>{
    return await fetchWrapper.get(`auctions/${id}`);
}