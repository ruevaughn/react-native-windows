import React, { Component } from 'react'
import PropTypes from 'prop-types'
import {
  Text,
  TouchableOpacity,
  View,
} from 'react-native'

import styles from './styles'

export default class TouchEvents extends Component {
  static propTypes = {
    logger: PropTypes.func
  }

  mouseDownHandler = (touchSource) => {
    this.props.logger(touchSource)
  }

  render() {
    return (
      <View style={{width: 100, height: 150, flex: 1}}>
        <View style={{width: 100, height: 50, flex: 1}}>
        <TouchableOpacity
          style={styles.outTouchableOpacityStyle}
          onPress={() => this.mouseDownHandler(`Touch from box-only`)}>
          <View pointerEvents={'box-only'}
            style={styles.innerViewStyle}
              >
            <Text>Box-only</Text>
          </View>
        </TouchableOpacity>
        </View>
        <View pointerEvents={'box-none'} style={{width: 100, height: 50, flex: 1}}>
        <TouchableOpacity
                style={styles.outTouchableOpacityStyle}
                onPress={() => this.mouseDownHandler(`Work!`)}>
                <View style={[styles.innerViewStyle, {backgroundColor: 'cornflowerBlue'}]}>
                  <Text> BOX-NONE </Text>
                </View>
        </TouchableOpacity>
        </View>
        <View pointerEvents={'box-only'} style={{width: 100, height: 50, flex: 1}}>
          <TouchableOpacity
                  style={styles.outTouchableOpacityStyle}
                  onPress={() => this.mouseDownHandler(`Does Not Work!`)}>
                  <View style={[styles.innerViewStyle, {backgroundColor: 'red'}]}>
                    <Text> BOX-ONLY </Text>
                  </View>
        </TouchableOpacity>
        </View>
      </View>
    )
  }
}