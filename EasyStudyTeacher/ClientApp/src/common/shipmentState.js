const ShipmentState = {
  /// Shipment is delivered to the parcel locker (Default state)
  Delivered: 0,

  /// Client has taken the shipment from the parcel locker
  Received: 1,

  /// Shipment hasn't been taked from the parcel locker in time (should be returned by expeditor)
  Expired: 2,

  /// Expeditor has taken the expired shipment from the parcel locker
  ToReturn: 3,

  /// Expeditor has taken the expired shipment from the parcel locker
  Returned: 4,

  /// Shipment wasn't taken by client or expeditor due to the parcel locker error
  Error: 5,
};
export default ShipmentState;
